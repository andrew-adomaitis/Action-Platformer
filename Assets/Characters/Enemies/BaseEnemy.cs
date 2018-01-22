using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class BaseEnemy : MonoBehaviour
{
    public float damage = 10f;

    HealthSystem player;

    void Awake()
    {
        player = FindObjectOfType<Player>().GetComponent<HealthSystem>();
    }

    public void DamagePlayer(float amount)
    {
        player.TakeDamage(damage);
    }
}
