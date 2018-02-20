using System.Collections;
using System.Collections.Generic;
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
    bool isStopping = false;
    bool canPatrol = true;
    bool isGoingUp = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distanceToTopPoint = Vector2.Distance(transform.position, topPoint.position);
        float distanceToBottomPoint = Vector2.Distance(transform.position, bottomPoint.position);

        if (canPatrol)
        {
            if (isGoingUp && distanceToTopPoint > stoppingDistance)
            {
                rb.velocity = Vector2.up * speed;
            }
            else if (!isGoingUp && distanceToBottomPoint > stoppingDistance)
            {

            }
        }
    }

    IEnumerator waitAtStoppingPoint()
    {
        isStopping = true;
        yield return new WaitForSecondsRealtime(waitTime);
        isStopping = false;
    }
}
