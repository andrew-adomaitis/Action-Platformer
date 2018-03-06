using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] float destroyDelay = 10f;
    
	void Start ()
    {
        StartCoroutine(DelayDestroy());
	}

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
    }
}
