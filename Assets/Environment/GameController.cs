using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float waitToRespawn = 2f;
        
    Player player;
    HealthSystem playerHealthSystem;
    ResetOnRespawn[] objectsToReset;
    CameraRig cameraRig;

    void Start()
    {
        player = FindObjectOfType<Player>();
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
        playerHealthSystem = player.gameObject.GetComponent<HealthSystem>();
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    public void RespawnNoDelay()
    {
        player.gameObject.SetActive(false);
        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }

    IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        playerHealthSystem.currentHP = playerHealthSystem.maxHP;
        playerHealthSystem.SetText();
        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
}