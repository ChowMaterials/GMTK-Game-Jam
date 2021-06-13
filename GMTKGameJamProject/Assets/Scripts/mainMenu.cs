using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public Transform Nimph;
    public Slider Volume;
    public float t;
    public void startButton()
    {
        t = 0;
        Debug.Log("Start");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    void Update()
    {
        t += Time.deltaTime;
        PlayerPrefs.SetFloat("MusicVolume", Volume.value);
        Nimph.position += new Vector3(0, Mathf.Sin(t), 0)*Time.deltaTime;
    }
    

}