using UnityEngine;
// TODO change architecture to prevent scaling of the health text
public class WalkTowardPlayer : MonoBehaviour
{
    [SerializeField] float speed = 4f;

    HealthSystem hs;
    Transform player;
    bool canChase = false;
    float scaleX;

    void Start()
    {
        hs = GetComponent<HealthSystem>();
        scaleX = transform.localScale.x;
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

            if(player.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-scaleX, transform.localScale.y);
            } else if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(scaleX, transform.localScale.y);
            }
        }
    }

    void LateUpdate()
    {
        hs.healthText.gameObject.transform.position = transform.position;
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