using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : ScreenComponent
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        text = GetComponent<Text>();

        if (text != null)
            text.enabled = false;
    }

    private void GameStart()
    {
        if (text != null)
            text.enabled = true;
    }

    private void GameOver()
    {
        if (text != null)
            text.enabled = false;
    }
}
