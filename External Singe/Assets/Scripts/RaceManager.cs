using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private float startTimer;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = 0.0f;
        startTime = 3.0f;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer >= startTime)
        {
            GameEventManager.StartRace();
        }

    }

    private void GameStart()
    {
        enabled = true;
    }

    private void GameOver()
    {
        enabled = false;
    }
}
