using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] string enemyTag;
    [SerializeField] float destroyDelay = 1f;

    Player player;
    Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();

        rb.gravityScale = 0;
    }

    void Update()
    {
        rb.AddRelativeForce(new Vector2(player.bulletSpeed, 0), ForceMode2D.Impulse);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == enemyTag)
        {
            HealthSystem enemyHealthSystem = other.gameObject.GetComponent<HealthSystem>();

            if(enemyHealthSystem != null)
            {
                enemyHealthSystem.TakeDamage(player.damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}