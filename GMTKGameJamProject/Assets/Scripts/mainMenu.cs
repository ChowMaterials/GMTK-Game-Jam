using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void startButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void quitButton()
    {
        Application.Quit();
    }

}