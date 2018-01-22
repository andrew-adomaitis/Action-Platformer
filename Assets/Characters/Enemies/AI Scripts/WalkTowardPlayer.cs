using UnityEngine;

public class WalkTowardPlayer : MonoBehaviour
{
    [SerializeField] float chaseRadius = 3f;
    [SerializeField] float speed = 4f;

    Transform player;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    
    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, player.position);
        if(distanceToTarget <= chaseRadius)
        {
            transform.position = Vector3.MoveTowards (
                transform.position, 
                new Vector3(player.position.x, transform.position.y, transform.position.z),
                speed
            );
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}