using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreRaceComponent : ScreenComponent
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.RaceStart += RaceStart;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    private void GameStart()
    {
        text.enabled = true;
    }

    private void  RaceStart()
    {
        text.enabled = false;
    }
}
