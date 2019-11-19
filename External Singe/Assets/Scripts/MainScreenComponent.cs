using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenComponent : ScreenComponent
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
        text = GetComponent<Text>();
    }

    private void GameStart()
    {
        text.enabled = false;
    }
}
