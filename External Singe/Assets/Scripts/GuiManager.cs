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
        running = true;
    }

    private void GameOver()
    {
        running = false;
    }

    private void RaceStart()
    {
       
    }
}
