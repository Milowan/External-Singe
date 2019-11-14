using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Text titleText;
    public Text instructionsText;
    public Text gameOverText;
    public Text lapText;
    public Text placeText;
    public Text countDownText;

    private bool firstRun;
    private bool running;

    public static int playerLap;
    public static int maxLaps;
    public static int startCountDown;
    public static int finished;


    // Start is called before the first frame update
    void Start()
    {
        titleText.enabled = true;
        instructionsText.enabled = true;
        gameOverText.enabled = false;
        lapText.enabled = false;
        placeText.enabled = false;
        countDownText.enabled = false;

        firstRun = true;
        running = false;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        GameEventManager.RaceStart += RaceStart;
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
        lapText.text = "Lap " + playerLap + "/" + maxLaps;
        countDownText.text = "" + startCountDown;
        placeText.text = "" + finished;
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
        lapText.enabled = true;
        placeText.enabled = false;
        countDownText.enabled = true;
        running = true;
        firstRun = false;
    }

    private void GameOver()
    {
        instructionsText.enabled = true;
        gameOverText.enabled = true;
        lapText.enabled = false;
        placeText.enabled = true;
        running = false;
    }

    private void RaceStart()
    {
        countDownText.enabled = false;
    }
}
