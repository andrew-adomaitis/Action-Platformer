using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthSystem))]
public class BaseEnemy : MonoBehaviour
{
    public float damage = 10f;

    Player player;
    HealthSystem playerHS;
    int amountOfBulletsHit;

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
        yield return new WaitForSeconds(.5f);

    }
}
