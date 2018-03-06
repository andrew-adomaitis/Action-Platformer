using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] string playerTag = "Player";
    [SerializeField] string groundTag = "Ground";
    [SerializeField] string deathParticles;

    [HideInInspector] public float speed = 0;

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
        rb.AddRelativeForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == groundTag)
        {
            print("Triggered with ground");
            Destroy(gameObject);
        }
        if (other.tag == enemyTag || other.tag == playerTag)
        {
            HealthSystem otherHS = other.gameObject.GetComponent<HealthSystem>();

            if(otherHS != null)
            {
                otherHS.TakeDamage(player.damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}