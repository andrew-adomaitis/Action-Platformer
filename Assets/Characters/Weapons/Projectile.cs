using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] string enemyTag;
    [SerializeField] float destroyDelay = 1f;

    Player player;
    
    void Awake()
    {
        player = FindObjectOfType<Player>();
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
