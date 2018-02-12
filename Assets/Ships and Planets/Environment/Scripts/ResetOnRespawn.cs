using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    Vector2 startTransform;
    Quaternion startRotation;

    void Start()
    {
        startTransform = transform.position;
        startRotation = Quaternion.identity;
    }

    public void ResetObjects()
    {
        transform.position = startTransform;
        transform.rotation = startRotation;
    }
}
