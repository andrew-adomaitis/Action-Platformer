using UnityEngine;

public class ChasePlayerInRadiusFly : MonoBehaviour
{
    [SerializeField] float speed = 4;

    float scaleX;
    bool canChase = false;
    Player target;
    
    void Start()
    {
        scaleX = transform.localScale.x;
        target = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(target.transform.position.x <= transform.position.x)
        {
            transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
        else if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-scaleX, transform.localScale.y);
        }

        if (canChase)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
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
