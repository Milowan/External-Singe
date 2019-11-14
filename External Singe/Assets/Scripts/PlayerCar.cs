using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : CarController
{
    private bool canLap;

    private void Awake()
    {
        canLap = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            torque = Input.GetAxis("Vertical");
            steerAngle = Input.GetAxis("Horizontal");
        }
        GuiManager.playerLap = lapCnt;
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
    }
}
