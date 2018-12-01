using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset;
    public Vector3 playerPosOffset;
    public float scrollSpeed = 2f;

    private GameObject player;
    private GameObject track;
    private Vector3 cameraPos;
    private Vector3 startingPos;
    private GameObject camWaypoint;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        track = GameObject.Find("Track");
        startingPos = new Vector3(0,0,-10);
        transform.position = startingPos;

        camWaypoint = GameObject.Find("camWaypoint");

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //transform.Translate(transform.position.x, scrollSpeed * Time.deltaTime, -10, Space.World);
        //cameraPos = new Vector3(transform.position.x, transform.position.y, -10);
        //transform.position = (cameraPos);

        if (camWaypoint != null)
        {
            Vector3 waypointPos = new Vector3(camWaypoint.transform.localPosition.x, camWaypoint.transform.localPosition.y, -10);
            transform.position = waypointPos;
        }

    }

}

