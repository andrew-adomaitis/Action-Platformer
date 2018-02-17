using UnityEngine;
// TODO change architecture to prevent scaling of the health text
public class WalkTowardPlayer : MonoBehaviour
{
    [SerializeField] float chaseRadius = 5;
    [SerializeField] float speed = 4f;
    [SerializeField] Transform objectToMove;

    Rigidbody2D rb;
    Transform player;
    bool canChase = false;
    float scaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleX = objectToMove.localScale.x;
        player = FindObjectOfType<Player>().transform;
    }
    
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if(distanceToPlayer <= chaseRadius)
        {
            canChase = true;
        } else
        {
            canChase = false;
        }
        
        HandleMovement();
    }

    void HandleMovement()
    {
        if (canChase)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(player.position.x, transform.position.y, transform.position.z),
                speed * Time.deltaTime
            );

            if (player.position.x > transform.position.x)
            {
                objectToMove.localScale = new Vector2(-scaleX, objectToMove.localScale.y);
                rb.velocity = Vector2.right * speed;
            }
            else if (player.position.x < transform.position.x)
            {
                objectToMove.localScale = new Vector2(scaleX, objectToMove.localScale.y);
                rb.velocity = Vector2.left * speed;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}