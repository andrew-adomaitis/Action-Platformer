﻿using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] string playerString = "Player";
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            boxCollider.isTrigger = true;
            StartCoroutine(ResetIsTrigger());
        }
    }
}
