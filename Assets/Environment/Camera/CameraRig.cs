using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform target;
    [Range(0, 5)][SerializeField] float followSpeed = .5f;
    
    void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
        }
    }

    void Update()
    {
        // Change for different axes?
        // Each frame we get X closer to the target
        transform.position += (target.position - transform.position) * followSpeed * Time.deltaTime;
    }
}