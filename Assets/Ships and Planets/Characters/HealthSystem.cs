﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public float maxHP = 100f;
    public Text healthText;
        
    [HideInInspector] public bool isKnockedBack = false;
    [HideInInspector] public float knockbackCounter;

    [HideInInspector] public float currentHP;
    GameController gameController;
    Player player;
    CameraShake cameraShake;

    public float HealthAsPercentage { get { return currentHP / maxHP; } }
    
    void Start()
    {
        currentHP = maxHP;
        SetText();
        gameController = FindObjectOfType<GameController>();
        player = GetComponent<Player>();
        cameraShake = gameController.gameObject.GetComponent<CameraShake>();
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);

        SetText();
                
        if (HealthAsPercentage <= Mathf.Epsilon)
        {
            if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
            }
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void SetText()
    {
        healthText.text = currentHP + " / " + maxHP;
    }

    void ShakeCamPlayerParams()
    {
        cameraShake.Shake(player.hurtCamShakeIntensity, player.hurtCamShakeLength, .1f);
    }
}