using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider red;
    public Slider green;
    public Slider blue;

    private PlayerCar player;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        red.enabled = false;
        green.enabled = false;
        blue.enabled = false;
        GameEventManager.MenuOpen += MenuOpen;
        GameEventManager.MenuClose += MenuClose;
    }

    public void SetPlayer(PlayerCar car)
    {
        player = car;
    }

    // Update is called once per frame
    void Update()
    {
        player.SetColour(red.value, green.value, blue.value);
    }

    private void MenuOpen()
    {
        enabled = true;
        red.enabled = true;
        green.enabled = true;
        blue.enabled = true;
    }

    private void MenuClose()
    {
        enabled = false;
        red.enabled = false;
        green.enabled = false;
        blue.enabled = false;
    }
}
