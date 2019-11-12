using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxelData> axels;
    private List<Transform> waypoints;
    public Transform nextWaypoint;
    protected Transform tf;
    private int targetWaypointIndex;
    protected float torque;
    protected float steerAngle;
    public float maxTorque;
    protected float maxSteerAngle;

    protected bool racing;

    private void Start()
    {
        maxSteerAngle = 30;
        GameObject[] points = GameObject.FindGameObjectsWithTag("Waypoint");
        racing = true;
        waypoints = new List<Transform>();
        if (points != null)
        {
            foreach (GameObject point in points)
            {
                waypoints.Add(point.GetComponent<Transform>());
            }
        }

        tf = GetComponent<Transform>();
        targetWaypointIndex = 0;
        nextWaypoint = waypoints[targetWaypointIndex];
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
                    targetWaypointIndex = 0;
                }
                nextWaypoint = waypoints[targetWaypointIndex];
            }
        }
    }
}