using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public Slider Volume;

    public void startButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    void Update()
    {
        PlayerPrefs.SetFloat("MusicVolume", Volume.value);
    }
    

}