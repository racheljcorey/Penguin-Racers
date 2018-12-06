using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour {

    private ScreenTransitionImageEffect screenTrans;
    public GameObject pauseMenu;
    private GameObject canvas;

    // Use this for initialization
    void Start () {

        screenTrans = Camera.main.GetComponent<ScreenTransitionImageEffect>();
        canvas = GameObject.Find("Canvas");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu = Instantiate(Resources.Load("Prefabs/UI/PauseMenu")) as GameObject;
        pauseMenu.transform.SetParent(canvas.transform, false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        Destroy(pauseMenu);
    }

    public void BackToMap()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitForFade());
    }

    public void ExitApplication()
    {
        Debug.Log("rip");
    }

    public IEnumerator WaitForFade()
    {
        StartCoroutine(screenTrans.FadeOut());
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").transform.GetChild(0).gameObject;
        Destroy(pauseMenu);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Map");
    }
}
