using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        if(target == null)
        {
            target = FindObjectOfType<Player>().gameObject.transform;
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
