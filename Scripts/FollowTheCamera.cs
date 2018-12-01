using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheCamera : MonoBehaviour {

    private GameObject cam;
    private Vector3 bgPos;

	// Use this for initialization
	void Start () {

        cam = GameObject.Find("Main Camera");
		
	}
	
	// Update is called once per frame
	void Update () {

        bgPos = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
        gameObject.transform.position = bgPos;
		
	}
}
