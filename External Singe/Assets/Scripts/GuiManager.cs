using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Text gameOverText;
    public Text instructionsText;
    public Text titleText;
    public Text placeText;

    private bool firstRun;
    private bool running;


    // Start is called before the first frame update
    void Start()
    {
        titleText.enabled = true;
        instructionsText.enabled = true;
        gameOverText.enabled = false;
        placeText.enabled = false;
        firstRun = true;
        running = false;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            if (Input.GetButtonDown("Jump"))
            {
                GameEventManager.TriggerGameStart();
            }
        }
    }

    private void GameStart()
    {
        if (firstRun)
        {
            titleText.enabled = false;
        }
        else
        {
            gameOverText.enabled = false;
        }
        instructionsText.enabled = false;
        placeText.enabled = true;
        running = true;
        firstRun = false;
    }

    private void GameOver()
    {
        placeText.enabled = false;
        instructionsText.enabled = true;
        gameOverText.enabled = true;
        running = false;
    }
}
