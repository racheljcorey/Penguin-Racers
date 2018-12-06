using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using cakeslice;

public class DetectClicks : MonoBehaviour {

    public bool clickedObject;
    private Ray ray;
    public RaycastHit hit;
    public string nameOfHit;
    private GameObject[] islands;
    private GameObject islandPopup;

    private Animator islandPopupAni;


	// Use this for initialization
	void Start () {

        islands = GameObject.FindGameObjectsWithTag("Island");
        
        
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Island")
            {
                nameOfHit = hit.transform.name;
                clickedObject = true;
            }
            else
            {
                clickedObject = false;
            }

            if (!clickedObject)
            { 
                foreach (GameObject island in islands)
                {
                    island.GetComponent<Outline>().eraseRenderer = true;
                }

                islandPopup = GameObject.FindGameObjectWithTag("Popup");
                if (islandPopup != null)
                {
                    islandPopupAni = islandPopup.GetComponent<Animator>();
                    islandPopupAni.SetBool("clickedOff", true);
                    StartCoroutine(windowWait());
                }
            }
           
        }

    }

    public IEnumerator windowWait()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(islandPopup);
    }

    public IEnumerator waitForFade(int scene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
