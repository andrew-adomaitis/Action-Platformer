using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;
    [SerializeField] float yOffsetFromTarget = 5;
    
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
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }

    public void ResetXPos()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + yOffsetFromTarget, -10);
    }
}