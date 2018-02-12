using UnityEngine;

public class WalkTowardPlayer : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    Transform player;
    bool canChase = false;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    
    void Update()
    {
        if(canChase)
        {
            transform.position = Vector3.MoveTowards (
                transform.position, 
                new Vector3(player.position.x, transform.position.y, transform.position.z),
                speed
            );
        }
    }

    void OnBecameVisible()
    {
        canChase = true;
    }

    void OnBecameInvisible()
    {
        canChase = false;
    }
}