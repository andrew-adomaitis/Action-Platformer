using System.Collections;
using UnityEngine;

public class SmarterChaseEnemy : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [Tooltip("How far to move toward the player")]
    [SerializeField] float chaseRadius = 10;
    [SerializeField] float bufferZone = 7;
    [Tooltip("When to start backing up from the player")]
    [SerializeField] float backupRadius = 3;
    [SerializeField] float speed = 3;
    [SerializeField] float chargeSpeed = 6;

    [Space]
    [Tooltip("How long to wait to stop charging")]
    [SerializeField] float timeToResetCharge = 1f;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform playerChargeCheck;

    enum State {backingUp, idle, chargeRight, chargeLeft, moveLeft, moveRight};
    State state = State.idle;

    float resetChargeTimer;
    float scaleX;
    float distanceToPlayer;
    bool wasShot = false;
    bool canChase = false;
    bool shouldCharge = false;
    bool canSwitchState = true;
    bool playerOnRight;
    Rigidbody2D rb;
    Transform player;
    BaseEnemy baseEnemy;

    void Awake()
    {
        resetChargeTimer = timeToResetCharge;
        scaleX = objectToMove.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        baseEnemy = GetComponent<BaseEnemy>();
        player = FindObjectOfType<Player>().gameObject.transform;
    }

    void Update()
    {
        HandleScaling();
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (canSwitchState)
        {
            HandleState();
        }
    }

    void HandleState()
    {
        float playerSpeed = player.GetComponent<Player>().speed;
        bool playerOutOfChaseRadius = (distanceToPlayer > chaseRadius && state != State.idle);
        playerOnRight = (player.position.x > transform.position.x);
        bool playerOutOfBackupRadius = (distanceToPlayer >= backupRadius);
        bool playerOutOfBufferZone = (distanceToPlayer >= bufferZone);
        
        if (playerOutOfChaseRadius && state != State.idle) // If the player's out of the chase radius
        {
            StopAllCoroutines();
            state = State.idle; // Do nothing
            rb.velocity = Vector2.zero;
        }
        // If the player's between the buffer zone and the chase radius
        else if (playerOutOfBufferZone && !playerOutOfChaseRadius && state != State.idle)
        {
            StopAllCoroutines();
            state = State.idle; // Do nothing
            rb.velocity = Vector2.zero;
        }
        // If the player's in between the backup radius and and the chase radius to the left
        else if (!playerOutOfChaseRadius && playerOutOfBackupRadius && !playerOnRight && state != State.moveLeft)
        {
            StopAllCoroutines();
            state = State.moveLeft; // move left
            StartCoroutine(ChasePlayer(-speed));
        }
        // If the player's in between the backup radius and and the chase radius to the right
        else if (!playerOutOfChaseRadius && playerOutOfBackupRadius && playerOnRight && state != State.moveRight)
        {
            StopAllCoroutines();
            state = State.moveRight; // move right
            StartCoroutine(ChasePlayer(speed));
        }
        // If the player is inside the backup radius and to the left
        else if (!playerOutOfBackupRadius && !playerOnRight && state != State.backingUp)
        {
            StopAllCoroutines();
            state = State.backingUp; // Backup right
            StartCoroutine(Backup(playerSpeed));
        }
        // If the player's inside the backup radius and to the right
        else if (!playerOutOfBackupRadius && playerOnRight && state != State.backingUp)
        {
            StopAllCoroutines();
            state = State.backingUp; // Backup left
            StartCoroutine(Backup(-playerSpeed));
        }
        // if we've been shot and the player's on the left
        else if (baseEnemy.hasBeenShot && !playerOnRight && state != State.chargeLeft)
        {
            resetChargeTimer = timeToResetCharge;
            StopAllCoroutines();
            state = State.chargeLeft;
            StartCoroutine(Charge(-chargeSpeed));
        }
        // if we've been shot and the player's on the right
        else if (baseEnemy.hasBeenShot && playerOnRight && state != State.chargeRight)
        {
            resetChargeTimer = timeToResetCharge;
            StopAllCoroutines();
            state = State.chargeRight;
            StartCoroutine(Charge(chargeSpeed));
        }
    }

    void HandleScaling()
    {
        if (playerOnRight)
        {
            objectToMove.localScale = new Vector2(scaleX, objectToMove.localScale.y);
        }
        else if (!playerOnRight)
        {
            objectToMove.localScale = new Vector2(-scaleX, objectToMove.localScale.y);
        }
    }

    IEnumerator ResetCharge()
    {
        yield return new WaitForEndOfFrame();
        canSwitchState = false;
        yield return new WaitForSecondsRealtime(timeToResetCharge);
        wasShot = false;
        canSwitchState = true;
    }

    IEnumerator ChasePlayer(float speed)
    {
        while (distanceToPlayer <= chaseRadius)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Backup(float speed)
    {
        while(distanceToPlayer <= backupRadius)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Charge(float speed)
    {
        while (resetChargeTimer >= Mathf.Epsilon)
        {
            canSwitchState = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            resetChargeTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canSwitchState = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bufferZone);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, backupRadius);
    }
}
