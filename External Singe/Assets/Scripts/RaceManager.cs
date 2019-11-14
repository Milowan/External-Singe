using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public float startTimer;
    private float startTime;
    private List<CarController> cars;
    private List<CarController> finishedcars;
    private CarController playerCar;
    public int maxLaps;

    // Start is called before the first frame update
    void Start()
    {
        GuiManager.maxLaps = maxLaps;
        startTimer = 0.0f;
        startTime = 3.0f;
        enabled = false;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        cars = new List<CarController>();
        finishedcars = new List<CarController>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            cars.Add(transform.GetChild(i).GetComponent<CarController>());
        }

        playerCar = cars[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer >= startTime)
        {
            GameEventManager.StartRace();
        }
        else
        {
            startTimer += Time.deltaTime;
            GuiManager.startCountDown = (int)(startTime - startTimer);
        }
        finishedcars.Clear();
        foreach (CarController car in cars)
        {
            if (car.lapCnt == maxLaps)
            {
                finishedcars.Add(car);
                if (car == playerCar)
                {
                    GameEventManager.TriggerGameOver();
                }
            }
        }
    }

    private void GameStart()
    {
        enabled = true;
        startTimer = 0;
    }

    private void GameOver()
    {
        for (int i = 0; i < finishedcars.Count; ++i)
        {
            if (finishedcars[i] == playerCar)
            {
                GuiManager.finished = i + 1;
            }
        }
        enabled = false;
    }
}
