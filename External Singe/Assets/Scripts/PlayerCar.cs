using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : CarController
{
    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            torque = Input.GetAxis("Vertical");
            steerAngle = Input.GetAxis("Horizontal");
        }
    }
}
