﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : CarController
{
    public float range;
    private List<Transform> waypoints;
    private GameObject[] containers;
    public Transform nextWaypoint;
    private int targetWaypointIndex;

    protected override void Init()
    {
        colour.r = Random.Range(0.0f, 255f);
        colour.g = Random.Range(0.0f, 255f);
        colour.b = Random.Range(0.0f, 255f);
        bodyMat.SetColor("_Color", colour);
        containers = GameObject.FindGameObjectsWithTag("Container");
        waypoints = new List<Transform>();
        SelectPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            torque = 1;
            Steer();
            RaycastHit hit;
            if (Physics.Raycast(tf.position, tf.forward, out hit, range) && hit.collider.gameObject.CompareTag("Car"))
            {
                boosting = true;
            }
        }

    }

    private void SelectPath()
    {
        GameObject container = containers[Random.Range(0, containers.Length)];
        for (int i = 0; i < container.transform.childCount; ++i)
        {
            waypoints.Add(container.transform.GetChild(i).transform);
        }
        targetWaypointIndex = 0;
        nextWaypoint = waypoints[targetWaypointIndex];
    }

    private void Steer()
    {

        Quaternion leftAngle = Quaternion.AngleAxis(maxSteerAngle, tf.up);
        Quaternion rightAngle = Quaternion.AngleAxis(-maxSteerAngle, tf.up);
        RaycastHit leftHit;
        RaycastHit rightHit;
        bool hitL = (Physics.Raycast(tf.position, leftAngle * tf.forward, out leftHit, range) && !(leftHit.collider.GetComponent<Terrain>() || leftHit.collider.gameObject.CompareTag("Waypoint")));
        bool hitR = (Physics.Raycast(tf.position, rightAngle * tf.forward, out rightHit, range) && !(rightHit.collider.GetComponent<Terrain>() || rightHit.collider.gameObject.CompareTag("Waypoint")));

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            if (nextWaypoint == other.GetComponent<Transform>())
            {
                targetWaypointIndex++;
                if (targetWaypointIndex >= waypoints.Count)
                {
                    ++lapCnt;
                    SelectPath();
                }
                nextWaypoint = waypoints[targetWaypointIndex];
            }
        }
        if (other.gameObject.CompareTag("RespawnPoint"))
        {
            respawnPoint = other.gameObject.transform;
            respawnTimer = 0;
        }

        if (other.gameObject.CompareTag("BoNoS"))
        {
            if (maxNoS < 4 * baseNoS)
            {
                maxNoS += baseNoS;
            }

            NoSUsed = 0.0f;
        }
    }
}
