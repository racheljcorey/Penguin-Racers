using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour

{
    private int _targetWaypoint = 0;
    private Transform _waypoints;
    private Rigidbody2D rb2d;

    public float movementSpeed = 0;

    // Use this for initialization
    void Start()
    {
        _waypoints = GameObject.Find("CameraWaypoints").transform;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fixed update
    void FixedUpdate()
    {
        handleWalkWaypoints();
    }

    // Handle walking the waypoints
    private void handleWalkWaypoints()
    {
        Transform targetWaypoint = _waypoints.GetChild(_targetWaypoint);
        Vector3 relative = targetWaypoint.position - transform.position;
        Vector3 movementNormal = Vector3.Normalize(relative);
        float distanceToWaypoint = relative.magnitude;
        float targetAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;

        if (distanceToWaypoint < 0.1)
        {
            if (_targetWaypoint + 1 < _waypoints.childCount)
            {
                // Set new waypoint as target
                _targetWaypoint++;
            }
        }
        else
        {
            // Walk towards waypoint
            rb2d.AddForce(new Vector2(movementNormal.x, movementNormal.y) * movementSpeed);
        }
    }
}
