using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pufferfish : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;
    private GameObject camWaypoint;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        camWaypoint = GameObject.Find("camWaypoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim = other.GetComponent<Animator>();
            StartCoroutine(Spin());
            sr.enabled = false;
        }
        if (other.tag == "Enemy")
        {
            anim = other.GetComponent<Animator>();
            StartCoroutine(SpinEnemy());
            sr.enabled = false;
        }
    }

    IEnumerator Spin()
    {
        anim.SetBool("spin", true);
        camWaypoint.SetActive(false);
        yield return new WaitForSeconds(.5f);
        camWaypoint.SetActive(true);
        anim.SetBool("spin", false);
        Destroy(gameObject);
    }

    IEnumerator SpinEnemy()
    {
        anim.SetBool("spin", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("spin", false);
        Destroy(gameObject);
    }
}
