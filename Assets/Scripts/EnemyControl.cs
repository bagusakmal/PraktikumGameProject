using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float detectionRange = 5f;
    private Transform player;
    private int currentPoint = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime * 2); // Speed up when chasing

        if (Vector3.Distance(transform.position, player.position) < 0.1f)
        {
            Debug.Log(Vector3.Distance(transform.position, player.position));
            Destroy(gameObject); // Destroy enemy after reaching player
        }
    }
}
