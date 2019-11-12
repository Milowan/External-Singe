using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : CarController
{
    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            torque = 1;
            Steer();
        }
    }

    private void Steer()
    {
        Vector3 target = nextWaypoint.position - tf.position;
        float angle = Vector3.SignedAngle(target, tf.forward, Vector3.up);

        angle *= -1;

        if (angle > maxSteerAngle)
        {
            steerAngle = 1;
        }
        else if (angle < -maxSteerAngle)
        {
            steerAngle = -1;
        }
        else
        {
            steerAngle = angle / maxSteerAngle;
        }

        if (Mathf.Abs(steerAngle) < 0.4f)
        {
            steerAngle = 0;
        }
    }
}
