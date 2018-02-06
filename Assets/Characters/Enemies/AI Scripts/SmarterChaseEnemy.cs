using UnityEngine;

public class SmarterChaseEnemy : MonoBehaviour
{
    [SerializeField] float backupRadius = 3;
    [SerializeField] float speed = 3;
    [SerializeField] float chargeSpeed = 6;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform playerChargeCheck;

    bool canChase = false;
    bool shouldCharge = false;
    Transform player;
    BaseEnemy baseEnemy;

    void Awake()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        player = FindObjectOfType<Player>().gameObject.transform;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (canChase)
        {
            if (baseEnemy.hasBeenShot)
            {
                shouldCharge = true;
            }
            if(distanceToPlayer <= backupRadius && shouldCharge) // Charge
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(playerChargeCheck.position.x, transform.position.y, transform.position.z),
                    chargeSpeed * Time.deltaTime
                );
            }
            if(distanceToPlayer <= backupRadius) // Move to Player
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(wallCheck.position.x, transform.position.y, transform.position.z),
                    player.GetComponent<Player>().speed * Time.deltaTime
                );
            }

            else
            {
                transform.position = Vector3.MoveTowards( // Backup
                    transform.position,
                    new Vector3(player.position.x, transform.position.y, transform.position.z),
                    speed * Time.deltaTime
                );
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, backupRadius);
    }

    void OnBecameVisible()
    {
        canChase = true;
    }
}
