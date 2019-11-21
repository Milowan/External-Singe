﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxelData> axels;
    protected Transform respawnPoint;
    protected Transform tf;
    private Rigidbody body;
    protected float torque;
    protected float steerAngle;
    public float baseTorque;
    public float brakeTorque;
    public float thrust;
    protected float maxSteerAngle;
    protected bool boosting;
    protected bool braking;

    protected float maxNoS;
    public float baseNoS;
    protected float NoSUsed;

    protected Color colour;
    protected Material bodyMat;

    protected bool racing;
    public float respawnTimer;
    public float timeToRespawn;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public int lapCnt;
    public bool finished;

    private void Awake()
    {
        lapCnt = 0;
        maxSteerAngle = 30;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        GameEventManager.RaceStart += RaceStart;
        tf = GetComponent<Transform>();
        body = GetComponent<Rigidbody>();
        bodyMat = transform.GetChild(0).GetComponent<Renderer>().material;
        colour = bodyMat.color;
        startPosition = tf.position;
        startRotation = tf.rotation;
        boosting = false;
        enabled = false;
        NoSUsed = 0.0f;
        Init();
    }

    protected virtual void Init()
    {

    }

    public void UpdateWheel(WheelCollider wheel)
    {
        if (wheel.transform.childCount != 0)
        {
            Transform tWheel = wheel.transform.GetChild(0);

            Vector3 position;
            Quaternion rotation;
            wheel.GetWorldPose(out position, out rotation);
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 90);

            tWheel.transform.position = position;
            tWheel.transform.rotation = rotation;
        }
    }

    public void FixedUpdate()
    {
        if (racing)
        {
            if (boosting)
            {
                Boost();
            }
            else if (NoSUsed > 0.0f)
            {
                NoSUsed -= Time.deltaTime / 2;
            }

            float motor = baseTorque * torque;
            float steering = maxSteerAngle * steerAngle;

            foreach (AxelData axel in axels)
            {
                if (axel.IsMotor())
                {
                    axel.GetLeft().motorTorque = motor;
                    axel.GetRight().motorTorque = motor;
                }
                if (axel.IsSteering())
                {
                    axel.GetLeft().steerAngle = steering;
                    axel.GetRight().steerAngle = steering;
                }
                UpdateWheel(axel.GetLeft());
                UpdateWheel(axel.GetRight());
            }

            if (braking)
            {
                Brake(brakeTorque);
            }
            else
            {
                Brake(0.0f);
            }
        }

        if (Vector3.Dot(tf.up, Vector3.down) > 0 || body.velocity == Vector3.zero)
        {
            respawnTimer += Time.deltaTime;
        }
        else
        {
            respawnTimer = 0;
        }

        if (respawnTimer >= timeToRespawn)
        {
            Respawn();
        }
    }


    private void Brake(float bTorque)
    {
        foreach (AxelData axel in axels)
        {
            if (axel.IsBraked())
            {
                axel.GetLeft().brakeTorque = bTorque;
                axel.GetRight().brakeTorque = bTorque;
            }
        }
    }


    protected virtual void Respawn()
    {
        tf.position = respawnPoint.position;
        tf.rotation = respawnPoint.rotation;
        maxNoS = baseNoS;
    }

    private void Boost()
    {
        if (NoSUsed < maxNoS)
        {
            body.AddForce(tf.forward * thrust, ForceMode.Acceleration);
            NoSUsed += Time.deltaTime;
        }
    }

    private void GameStart()
    {
        maxNoS = baseNoS;
        tf.position = startPosition;
        tf.rotation = startRotation;
        body.velocity.Set(0.0f, 0.0f, 0.0f);
        racing = false;
        finished = false;
    }

    private void GameOver()
    {
        enabled = false;
        lapCnt = 0;
    }

    private void RaceStart()
    {
        enabled = true;
        racing = true;
    }
}