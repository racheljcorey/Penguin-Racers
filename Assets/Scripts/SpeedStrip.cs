using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStrip : MonoBehaviour
{
    private CameraFollow camSpeed;

    private void Awake()
    {
        camSpeed = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SpeedUp());
    }

    IEnumerator SpeedUp()
    {
        camSpeed.scrollSpeed += 5;
        yield return new WaitForSeconds(4);
        camSpeed.scrollSpeed -= 5;
    }

}