using System.Collections;
using UnityEngine;

public class SmarterChaseEnemy : MonoBehaviour
{
    [Tooltip("How far to move toward the player")]
    [SerializeField] float chaseRadius = 10;
    [Tooltip("When to start backing up from the player")]
    [SerializeField] float backupRadius = 3;
    [SerializeField] float speed = 3;
    [SerializeField] float chargeSpeed = 6;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform playerChargeCheck;

    enum State {backupRight, backupLeft, idle, chargeRight, chargeLeft, moveLeft, moveRight};
    State state = State.idle;

    float scaleX;
    float distanceToPlayer;
    bool wasShot = false;
    bool canChase = false;
    bool shouldCharge = false;
    Rigidbody2D rb;
    Transform player;
    BaseEnemy baseEnemy;

    void Awake()
    {
        scaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        baseEnemy = GetComponent<BaseEnemy>();
        player = FindObjectOfType<Player>().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (baseEnemy.hasBeenShot == true)
        {
            wasShot = true;
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        HandleState();
    }

    void HandleState()
    {
        
        if (distanceToPlayer > chaseRadius && state != State.idle) // If the player's out of the chase radius
        {
            StopAllCoroutines();
            state = State.idle; // Do nothing
            rb.velocity = Vector2.zero;
        }
        // If the player's in between the backup radius and and the chase radius to the left
        else if (distanceToPlayer <= chaseRadius && distanceToPlayer >= backupRadius && player.position.x <= transform.position.x && state != State.moveLeft)
        {
            StopAllCoroutines();
            state = State.moveLeft; // move left
            StartCoroutine(ChasePlayer(-speed));
        }
        // If the player's in between the backup radius and and the chase radius to the left
        else if (distanceToPlayer <= chaseRadius && distanceToPlayer >= backupRadius && player.position.x > transform.position.x && state != State.moveRight)
        {
            StopAllCoroutines();
            state = State.moveRight; // move right
            StartCoroutine(ChasePlayer(speed));
        }
        // If the player is inside the backup radius and to the left
        else if (distanceToPlayer <= backupRadius && player.position.x <= transform.position.x && state != State.backupRight)
        {
            StopAllCoroutines();
            state = State.backupRight; // Backup right
            StartCoroutine(Backup(player.GetComponent<Player>().speed));
        }
        // If the player's inside the backup radius and to the right
        else if (distanceToPlayer <= backupRadius && player.position.x > transform.position.x && state != State.backupLeft)
        {
            StopAllCoroutines();
            state = State.backupLeft; // Backup left
            StartCoroutine(Backup(player.GetComponent<Player>().speed * -1));
        }
        // if we've been shot and the player's on the left
        else if (wasShot == true && distanceToPlayer <= backupRadius && player.position.x <= transform.position.x && state != State.chargeLeft)
        {
            StopAllCoroutines();
            state = State.chargeLeft;
            StartCoroutine(Charge(-chargeSpeed));
        }
        // if we've been shot and the player's on the right
        else if (wasShot == true && distanceToPlayer <= backupRadius && player.position.x > transform.position.x && state != State.chargeRight)
        {
            StopAllCoroutines();
            state = State.chargeRight;
            StartCoroutine(Charge(chargeSpeed));
        }
    }

    IEnumerator ChasePlayer(float speed)
    {
        while (distanceToPlayer <= chaseRadius)
        {
            rb.velocity = Vector2.right * speed;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Backup(float speed)
    {
        while(distanceToPlayer <= backupRadius)
        {
            rb.velocity = Vector2.right * speed;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Charge(float speed)
    {
        while (wasShot == true)
        {
            rb.velocity = Vector2.right * speed;
            yield return new WaitForEndOfFrame();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, backupRadius);
    }
}
