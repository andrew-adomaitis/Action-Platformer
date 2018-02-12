using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthSystem))]
public class BaseEnemy : MonoBehaviour
{
    public int damage = 10;
    [SerializeField] string projectileTag = "Projectile";

    [HideInInspector] public int damageTaken; // Number of bullets hit
    [HideInInspector] public bool hasBeenShot = false;

    Player player;
    HealthSystem playerHS;
    bool isDamageReseting = false;
    
    void Awake()
    {
        player = FindObjectOfType<Player>();
        playerHS = player.GetComponent<HealthSystem>();
    }
    
    public void DamagePlayer(float amount)
    {
        playerHS.TakeDamage(damage);
    }

    IEnumerator WaitToResetDamageCount()
    {
        isDamageReseting = true;
        yield return new WaitForSeconds(.5f);
        damageTaken = 0; // Reset the amount of bullets hit
        isDamageReseting = false;
        hasBeenShot = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == projectileTag)
        {
            hasBeenShot = true;
            damageTaken += player.damage;
            if (damageTaken >= player.damage * player.numOfBullets)
                player.ActivateConditionOnEnemy();
            if(!isDamageReseting)
                StartCoroutine(WaitToResetDamageCount());
        }
    }
}
