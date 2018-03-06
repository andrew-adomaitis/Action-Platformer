using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float shootRadius = 13;
    [SerializeField] float reloadTime = .5f;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform turretShooter;
    [SerializeField] Transform gunEnd;

    bool canFire = false;
    bool isReloading = false;
    Transform player;
    
    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        GameObject bullet;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        FaceTarget(player);

        if (canFire && !isReloading)
        {
            bullet = Instantiate(bulletPrefab, gunEnd.position, gunEnd.rotation);
            bullet.GetComponent<Projectile>().speed = -bulletSpeed;
            StartCoroutine(CountDownTimeBetweenHits(reloadTime));
        }

        if(distanceToPlayer >= shootRadius)
        {
            canFire = false;
        }
        else if (distanceToPlayer < shootRadius && !isReloading)
        {
            canFire = true;
        }
    }

    IEnumerator CountDownTimeBetweenHits(float timeToWait)
    {
        isReloading = true;
        canFire = false;
        yield return new WaitForSeconds(timeToWait);
        isReloading = false;
    }

    void FaceTarget(Transform target)
    {
        Vector3 diff = target.position - turretShooter.position; // Find the target's position
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Find out the target's position relative to the player
        turretShooter.rotation = Quaternion.Euler(0f, 0f, rot_z - 180); // Rotate toward the player
    }

    void OnDrawGizmos()
    {
        // Draw the gun end sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);

        // Draw the gun end gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gunEnd.position, .1f);
    }
}