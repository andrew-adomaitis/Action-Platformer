using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerInRadiusFly : MonoBehaviour
{
    [SerializeField] float speed = 4;

    bool canChase = false;
    Player target;
    
    void Start()
    {
        target = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (canChase)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnBecameVisible()
    {
        canChase = true;
    }

    void OnBecameInvisible()
    {
        canChase = false;
    }
}
