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
    private bool inMenu;

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

        inMenu = false;
        running = false;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            if (!inMenu)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    GameEventManager.TriggerGameStart();
                }
                if (Input.GetButtonDown("Options"))
                {
                    GameEventManager.OpenMenu();
                }
            }
            else if (Input.GetButtonDown("Options"))
            {
                GameEventManager.CloseMenu();
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

    private void MenuOpen()
    {
        inMenu = true;
    }

    private void MenuClose()
    {
        inMenu = false;
    }
}
