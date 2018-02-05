using UnityEngine;

public class SmarterChaseEnemy : MonoBehaviour
{
    [SerializeField] float backupRadius = 3;
    [SerializeField] float speed = 3;
    [SerializeField] float chargeSpeed = 6;
    [SerializeField] Transform wallCheck;

    bool canChase = false;
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
            if(distanceToPlayer <= backupRadius && baseEnemy.hasBeenShot)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(player.position.x, transform.position.y, transform.position.z),
                    chargeSpeed * Time.deltaTime
                );
            }
            if(distanceToPlayer <= backupRadius)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(wallCheck.position.x, transform.position.y, transform.position.z),
                    player.GetComponent<Player>().speed * Time.deltaTime
                );
            }

            else
            {
                transform.position = Vector3.MoveTowards(
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
