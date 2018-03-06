using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject inGameUI;

    public GameObject quitMenuUI;

    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }          
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        player.isGrounded = false;
        pauseMenuUI.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        quitMenuUI.SetActive(true);
    }

    public void QuitMenuBack()
    {
        quitMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    
    
}
