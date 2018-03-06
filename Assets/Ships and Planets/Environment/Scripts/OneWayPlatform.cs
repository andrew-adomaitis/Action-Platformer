using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] float waitToResetTriggerLength = .5f;

    Collider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    IEnumerator ResetIsTrigger()
    {
        yield return new WaitForSecondsRealtime(waitToResetTriggerLength);
        boxCollider.isTrigger = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (Input.GetAxisRaw("Vertical") < 0 && other.collider.tag == playerTag)
        {
            boxCollider.isTrigger = true;
            StartCoroutine(ResetIsTrigger());
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Input.GetAxisRaw("Vertical") < 0 && other.collider.tag == playerTag)
        {
            boxCollider.isTrigger = true;
            StartCoroutine(ResetIsTrigger());
        }
    }
}
