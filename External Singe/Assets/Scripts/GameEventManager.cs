using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{

    public delegate void GameEvent();

    public static event GameEvent GameStart;
    public static event GameEvent GameOver;
    public static event GameEvent RaceStart;
    public static event GameEvent MenuOpen;
    public static event GameEvent MenuClose;
    public static void TriggerGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }

    public static void TriggerGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }

    public static void StartRace()
    {
        if (RaceStart != null)
        {
            RaceStart();
        }
    }

    public static void OpenMenu()
    { 
        if (MenuOpen != null)
        {
            MenuOpen();
        }
    }

    public static void CloseMenu()
    {
        if (MenuClose != null)
        {
            MenuClose();
        }
    }
}

