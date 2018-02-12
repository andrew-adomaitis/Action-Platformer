using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] float speed = 7;
    [SerializeField] float chaseRadius = 5;

    PlayerShipControl target;

    void Start()
    {
        target = FindObjectOfType<PlayerShipControl>();
    }

    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
