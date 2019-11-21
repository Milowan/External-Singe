using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlider : MonoBehaviour
{
    private RectTransform fill;
    private RectTransform handle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        GameEventManager.MenuOpen += MenuOpen;
        GameEventManager.MenuClose += MenuClose;
    }

    private void MenuOpen()
    {
        gameObject.SetActive(true);
    }

    private void MenuClose()
    {
        gameObject.SetActive(false);
    }
}
