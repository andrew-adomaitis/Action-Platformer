using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] string projectileTag = "Projectile";
    [SerializeField] string playerTag = "Player";
    [SerializeField] GameObject deathParticles;

    bool hasBeenShot = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == projectileTag)
        {
            if (!hasBeenShot)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                hasBeenShot = true;
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {        
        if(other.collider.tag == playerTag)
        {
            print("Collided with player");
        }
    }
}
