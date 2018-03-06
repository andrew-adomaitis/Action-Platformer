using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [SerializeField] GameObject waypointContainer;
    [SerializeField] float moveSpeed = 1;
    [Tooltip("How close to a waypoint to be able to stop")]
    [SerializeField] float waypointTolerance = .2f;
    [Tooltip("How long to stop at a waypoint")]
    [SerializeField] float waypointDwellTime = .5f;

    int nextWaypointIndex;
    bool canPatrol = true;

    void Start()
    {
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (canPatrol)
        {
            Vector2 nextWaypointPos = waypointContainer.transform.GetChild(nextWaypointIndex).position;
            transform.position = Vector3.MoveTowards(transform.position, nextWaypointPos, moveSpeed * Time.deltaTime);
            CycleWaypointWhenClose(nextWaypointPos);
            yield return new WaitForEndOfFrame();
        }
    }

    void CycleWaypointWhenClose(Vector3 nextWaypointPos)
    {
        if (Vector3.Distance(transform.position, nextWaypointPos) <= waypointTolerance)
        {
            nextWaypointIndex = (nextWaypointIndex + 1) % waypointContainer.transform.childCount;
        }
    }

    void OnDrawGizmos()
    {
        Vector2 firstPosition = waypointContainer.transform.GetChild(0).position;
        Vector2 previousPosition = firstPosition;
        foreach (Transform waypoint in waypointContainer.transform)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(waypoint.position, 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, firstPosition);
    }
}
