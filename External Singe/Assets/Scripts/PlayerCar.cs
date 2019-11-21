using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : CarController
{
    private bool canLap;

    private Slider NoSSlider;

    protected override void Init()
    {
        canLap = false;
        NoSSlider = GameObject.Find("NoSMeter").GetComponent<Slider>();
        GameObject.Find("Options").GetComponent<Menu>().SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            torque = Input.GetAxis("Vertical");
            steerAngle = Input.GetAxis("Horizontal");
            if (Input.GetButton("Boost"))
            {
                boosting = true;
            }
            else
            {
                boosting = false;
            }
            if (Input.GetButton("Brake"))
            {
                braking = true;
            }
            else
            {
                braking = false;
            }
            NoSSlider.value = 1 - (NoSUsed / maxNoS);
        }
        GuiManager.playerLap = lapCnt;
    }

    public void SetColour(float r, float g, float b)
    {
        colour.r = r;
        colour.g = g;
        colour.b = b;
        bodyMat.SetColor("_Color", colour);
    }

    protected override void Respawn()
    {
        base.Respawn(); 
        NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta.x * ((maxNoS / baseNoS) + 1), NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PWaypoint"))
        {
            canLap = true;
        }
        if (other.gameObject.CompareTag("RespawnPoint"))
        {
            respawnPoint = other.gameObject.transform;
            respawnTimer = 0;
        }
        if (other.gameObject.CompareTag("Finish") && canLap)
        {
            lapCnt++;
            canLap = false;
        }
        if (other.gameObject.CompareTag("BoNoS"))
        {
            if (maxNoS < 4 * baseNoS)
            {
                maxNoS += baseNoS;
                NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta.x * ((maxNoS / baseNoS)), NoSSlider.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }

            NoSUsed = 0.0f;
        }
    }
}
