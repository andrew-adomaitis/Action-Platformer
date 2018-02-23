using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretShooter;

    BaseEnemy baseEnemy;
    Transform player;
    
    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        baseEnemy = GetComponent<BaseEnemy>();
    }

    void Update()
    {
        Vector3 diff = player.position - turretShooter.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        turretShooter.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
    }
}