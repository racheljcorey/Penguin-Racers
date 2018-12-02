using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClicks : MonoBehaviour {

    public bool clickedObject;
    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
	
	// Update is called once per frame
	void Update () {

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Island")
            {
                Debug.Log("This is an Island");
            }
            else
            {
                Debug.Log("This isn't an Island");
            }
        }

    }
}
