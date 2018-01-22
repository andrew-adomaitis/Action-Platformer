using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
        }
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        
    }
}