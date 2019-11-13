﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxelData> axels;
    protected Transform respawnPoint;
    protected Transform tf;
    protected float torque;
    protected float steerAngle;
    public float maxTorque;
    protected float maxSteerAngle;

    protected bool racing;
    public float respawnTimer;
    public float timeToRespawn;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        maxSteerAngle = 30;
        GameEventManager.GameStart += GameStart;
        GameEventManager.RaceStart += RaceStart;
        tf = GetComponent<Transform>();
        startPosition = tf.position;
        startRotation = tf.rotation;
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
            float motor = maxTorque * torque;
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
        }

        if (Vector3.Dot(tf.up, Vector3.down) > 0)
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


    private void Respawn()
    {
        tf.position = respawnPoint.position;
        tf.rotation = respawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RespawnPoint"))
        {
            respawnPoint = other.gameObject.transform;
            respawnTimer = 0;
        }
    }

    private void GameStart()
    {
        tf.position = startPosition;
        tf.rotation = startRotation;
        racing = false;
    }

    private void RaceStart()
    {
        racing = true;
    }
}