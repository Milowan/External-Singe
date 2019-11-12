using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxelData
{
    public WheelCollider left;
    public WheelCollider right;
    public bool motor;
    public bool steering;

    public bool IsMotor()
    {
        return motor;
    }

    public bool IsSteering()
    {
        return steering;
    }

    public WheelCollider GetLeft()
    {
        return left;
    }

    public WheelCollider GetRight()
    {
        return right;
    }
}