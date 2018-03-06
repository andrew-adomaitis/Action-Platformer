using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Elevator : MonoBehaviour
{
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] float speed = 5;
    [SerializeField] float stoppingDistance = 1;
    [SerializeField] float waitTime = 5;

    Rigidbody2D rb;
    bool canPatrol = true;
    bool isGoingUp = true;
    bool isPatrolling = false;
    float distanceToTopPoint;
    float distanceToBottomPoint;
    Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        distanceToTopPoint = Vector2.Distance(transform.position, topPoint.position);
        distanceToBottomPoint = Vector2.Distance(transform.position, bottomPoint.position);

        if (canPatrol && !isPatrolling)
        {
            StartCoroutine(PatrolUp());
        }
    }

    IEnumerator PatrolUp()
    {
        isPatrolling = true;
        isGoingUp = true;
        while (distanceToTopPoint > stoppingDistance)
        {
            rb.velocity = Vector2.up * speed;
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(waitTime);
        yield return StartCoroutine(PatrolDown());
    }

    IEnumerator PatrolDown()
    {
        isPatrolling = true;
        isGoingUp = false;
        while (distanceToBottomPoint > stoppingDistance)
        {
            rb.velocity = Vector2.down * speed;
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(waitTime);
        yield return StartCoroutine(PatrolUp());
    }


    IEnumerator WaitAtStoppingPoint()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        if (isGoingUp)
        {
            isGoingUp = false;
            StartCoroutine(PatrolDown());
        }
        else
        {
            isGoingUp = true;
            StartCoroutine(PatrolUp());
        }
        isPatrolling = false;
    }
}
