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
        cameraRig = FindObjectOfType<CameraRig>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    IEnumerator RespawnCo()
    {
        player.canMove = false; // Stop the player from controlling the character
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset the player's velocity
        yield return new WaitForSeconds(waitToRespawn); // Wait for a while for the player to realize that they've died
        playerHealthSystem.currentHP = playerHealthSystem.maxHP; // Reset the player's health
        playerHealthSystem.SetText(); // Reset the player's health text
        // Reset the player's velocity. Necessary to do twice to stop the player from falling
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = player.respawnPos; // Reset the player's position
        cameraRig.ResetXPos(); // Reset the camera position
        player.canMove = true; // Allow the player to move again
    }
}