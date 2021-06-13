using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{

    public int scoreValue;
    public float timer = 0.0f;
    public int secondsModifier;
    private int givePoints;

    private void Start()
    {
        secondsModifier = 1;
        givePoints = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 20)
            secondsModifier = 2;
        if (timer > 60)
            secondsModifier = 5;
        if (timer > 120)
            secondsModifier = 10;
        if (timer > 180)
            secondsModifier = 20;
        if (timer > 240)
            secondsModifier = 50;
        if (timer > 300)
            secondsModifier = 100;
        if ((int)timer > givePoints)
        {
            scoreValue += GameObject.FindGameObjectsWithTag("tree").Length * secondsModifier;
            givePoints++;
        }

        GameObject.Find("Score").GetComponentInChildren<Text>().text = "Score " + scoreValue;

    }

}
