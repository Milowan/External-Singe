using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenComponent : ScreenComponent
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        text = GetComponent<Text>();
        text.enabled = false;
    }

    private void GameStart()
    {
        text.enabled = false;
    }

    private void GameOver()
    {
        text.enabled = true;
    }
}
