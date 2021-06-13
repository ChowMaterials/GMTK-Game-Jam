using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public GameObject restart;
    public GameObject resume;
    public GameObject menu;
    public GameObject death;

    void Start()
    {
        resumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                pauseGame();
            else
                resumeGame();
        }
        if (PlayerBehaviour.isPlayerDead)
        {
            restart.SetActive(true); 
            menu.SetActive(true);
            death.SetActive(true);
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        resume.SetActive(true);
        menu.SetActive(true);

    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        restart.SetActive(false);
        resume.SetActive(false);
        menu.SetActive(false);
        death.SetActive(false);

    }

    public void restartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
