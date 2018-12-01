using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour {

    private DragRigidbody playerSpeed;
    private FollowWaypoints camWaypoint;
    private CameraFollow camSpeed;
    private EnemyFollowWaypoints enemySpeed;

    private void Awake()
    {
        playerSpeed = GameObject.Find("Player").GetComponent<DragRigidbody>();
        enemySpeed = GameObject.Find("Enemy").GetComponent<EnemyFollowWaypoints>();
        camSpeed = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        camWaypoint = GameObject.Find("camWaypoint").GetComponent<FollowWaypoints>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SlowPlayer());
        }
        if (other.tag == "Enemy")
        {
            StartCoroutine(SlowEnemy());
        }
    }

    IEnumerator SlowPlayer()
    {
        playerSpeed.force = 100;
        playerSpeed.damping = 50;
        camSpeed.scrollSpeed -= 3;
        camWaypoint.movementSpeed -= 30;
        yield return new WaitForSeconds(1.5f);
        camWaypoint.movementSpeed += 30;
        playerSpeed.force = 800;
        playerSpeed.damping = 0;
        camSpeed.scrollSpeed += 3;
    }

    IEnumerator SlowEnemy()
    {
        enemySpeed.movementSpeed -= 70;
        yield return new WaitForSeconds(1.5f);
        enemySpeed.movementSpeed += 70;
    }
}
