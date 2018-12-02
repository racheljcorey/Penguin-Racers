using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DetectClicks : MonoBehaviour {

    public bool clickedObject;
    private Ray ray;
    private RaycastHit hit;
    private GameObject[] islands;
    private GameObject island;


	// Use this for initialization
	void Start () {

        islands = GameObject.FindGameObjectsWithTag("Island");
        island = GameObject.FindGameObjectWithTag("Island");
        
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Island")
            {
                    Debug.Log("This is an Island");
                clickedObject = true;
            }
            else
            {
                Debug.Log("This isn't an Island");
                clickedObject = false;
            }

            //if (!clickedObject)
            //{
            //    foreach (GameObject island in islands)
            //    {
            //        island.GetComponent<Outline>().eraseRenderer = true;
            //    }
            //}

            if (island.GetComponent<Outline>() != null)
            {
                Debug.Log("test");
            }
        }




    }
}
