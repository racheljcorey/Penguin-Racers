using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowWaypoints : MonoBehaviour {


    private int _targetWaypoint = 0;
    private Transform _waypoints;
    private Rigidbody rb;
    private Transform targetWaypoint;
    private Vector3 relative;
    private Vector3 movementNormal;
    private float distanceToWaypoint;
    private float targetAngle;
    public float movementSpeed = 3f;

    //Set this to the transform you want to check
    private Transform objectTransfom;

    private float noMovementThreshold = 0.0001f;
    private const int noMovementFrames = 3;
    Vector3[] previousLocations = new Vector3[noMovementFrames];
    public bool isMoving;

    // Use this for initialization
    void Start()
    {
        _waypoints = GameObject.Find("EnemyWaypoints").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        objectTransfom = gameObject.GetComponent<Transform>();
    }

    // Fixed update
    void FixedUpdate()
    {
        Vector3 zPosAdj = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -.15f);
        gameObject.transform.position = zPosAdj;
        HandleWalkWaypoints();
        CheckIfMoving();
    }

    // Handle walking the waypoints
    private void HandleWalkWaypoints()
    {
        targetWaypoint = _waypoints.GetChild(_targetWaypoint);
        relative = targetWaypoint.position - transform.position;
        movementNormal = Vector3.Normalize(relative);
        distanceToWaypoint = relative.magnitude;
        targetAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;
        if (distanceToWaypoint < 2)
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
            rb.AddForce(new Vector2(movementNormal.x, movementNormal.y) * movementSpeed);
        }
        // Face walk direction
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    //Let other scripts see if the object is moving
    public bool IsMoving
    {
        get { return isMoving; }
    }

    void Awake()
    {
        //For good measure, set the previous locations
        for (int i = 0; i < previousLocations.Length; i++)
        {
            previousLocations[i] = Vector3.zero;
        }
    }

    void CheckIfMoving()
    {
        //Store the newest vector at the end of the list of vectors
        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            previousLocations[i] = previousLocations[i + 1];
        }
        previousLocations[previousLocations.Length - 1] = objectTransfom.position;

        //Check the distances between the points in your previous locations
        //If for the past several updates, there are no movements smaller than the threshold,
        //you can most likely assume that the object is not moving
        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
            {
                //The minimum movement has been detected between frames
                isMoving = true;
                break;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
