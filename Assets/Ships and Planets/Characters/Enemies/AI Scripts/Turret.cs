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
    BaseEnemy baseEnemy;
    Transform player;
    
    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        baseEnemy = GetComponent<BaseEnemy>();
    }

    void Update()
    {
        FaceTarget(player);
    }

    IEnumerator CountDownTimeBetweenHits(float timeToWait)
    {
        yield return null;
    }

    void FaceTarget(Transform target)
    {
        Vector3 diff = target.position - turretShooter.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        turretShooter.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
    }

    void OnDrawGizmos()
    {
        // Draw the 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);

        // Draw the gun end gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gunEnd.position, .1f);
    }
}