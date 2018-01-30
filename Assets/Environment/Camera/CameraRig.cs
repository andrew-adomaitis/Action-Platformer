using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;
    [SerializeField] float followSpeed = 10f;
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
        transform.position = Vector2.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
    }

    public void ResetXPos()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + yOffsetFromTarget, -10);
    }
}