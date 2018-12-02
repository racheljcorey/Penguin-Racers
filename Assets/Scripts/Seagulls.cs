using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagulls : MonoBehaviour {

    private Animator anim;
    private GameObject camWaypoint;
    private EnemyFollowWaypoints enemySpeed;
    private bool coRun;

    private bool isInCollision;

    private void Awake()
    {
        camWaypoint = GameObject.Find("camWaypoint");
        enemySpeed = GameObject.Find("Enemy").GetComponent<EnemyFollowWaypoints>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInCollision == false)
        {
            if (coRun == false)
            {
                if (other.tag == "Player")
                {
                    isInCollision = true;
                    StartCoroutine(SlowPlayer());
                }
                if (other.tag == "Enemy")
                {
                    isInCollision = true;
                    StartCoroutine(SlowEnemy());
                }
            }
        }
        else
        {
            isInCollision = false;
        }
    }

    IEnumerator SlowPlayer()
    {
        coRun = true;
        camWaypoint.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        camWaypoint.SetActive(true);
        coRun = false;
        Destroy(gameObject);
    }

    IEnumerator SlowEnemy()
    {
        coRun = true;
        enemySpeed.movementSpeed -= 70;
        yield return new WaitForSeconds(2.5f);
        enemySpeed.movementSpeed += 70;
        coRun = false;
        Destroy(gameObject);
    }
}
