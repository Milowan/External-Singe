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
        fill = GetComponent<Slider>().fillRect;
        handle = GetComponent<Slider>().handleRect;
        handle.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        fill.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    private void MenuOpen()
    {
        handle.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        fill.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    private void MenuClose()
    {
        handle.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        fill.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }
}
