using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;

    float shakeAmount = 0;

    void Awake()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void Shake(float amount, float length, float frequency)
    {
        shakeAmount = amount;
        InvokeRepeating("StartShake", 0, frequency);
        Invoke("StopShake", length);
    }

    void StartShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            float shakePosX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakePosY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += shakePosX;
            camPos.y += shakePosY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
