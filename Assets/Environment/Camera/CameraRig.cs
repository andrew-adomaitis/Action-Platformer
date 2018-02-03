using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;
    [SerializeField] float yOffsetFromTarget = 5;
    [SerializeField] float followAhead = 5;
    
    void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
        }
        yOffsetFromTarget = transform.position.y - target.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(target.position.x, target.position.y);
        //if(target.localScale.x > 0)
        //    transform.position = new Vector2(target.position.x + followAhead, target.position.y);
        //else if (target.localScale.x < 0)
        //    transform.position = new Vector2(target.position.x - followAhead, target.position.y);
    }

    public void ResetXPos()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + yOffsetFromTarget, -10);
    }
}