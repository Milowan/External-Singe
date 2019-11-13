using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : CarController
{
    public float range;

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

        Quaternion leftAngle = Quaternion.AngleAxis(maxSteerAngle, tf.up);
        Quaternion rightAngle = Quaternion.AngleAxis(-maxSteerAngle, tf.up);
        RaycastHit leftHit;
        RaycastHit rightHit;
        bool hitL = (Physics.Raycast(tf.position, leftAngle * tf.forward, out leftHit, range) && !(leftHit.collider.GetComponent<Terrain>()));
        bool hitR = (Physics.Raycast(tf.position, rightAngle * tf.forward, out rightHit, range) && !(rightHit.collider.GetComponent<Terrain>()));
        Debug.DrawRay(tf.position, leftAngle * tf.forward * range);
        Debug.DrawRay(tf.position, rightAngle * tf.forward * range);

        if (!hitL && !hitR)
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
        else if (leftHit.collider != null)
        {
            steerAngle = 1;
        }
        else if (rightHit.collider != null)
        {
            steerAngle = -1;
        }

        
    }
}
