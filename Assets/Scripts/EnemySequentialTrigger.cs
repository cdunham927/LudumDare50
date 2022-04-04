using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequentialTrigger : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public EnemyController enemy;
    public bool startSpawning = false;
    public bool finishedSpawning;
    public int pos = 0;
    public float timeBetweenSpawns = 3f;
    float curTimer = 0f;

    public void ResetTime()
    {
        curTimer = 0f;
        startSpawning = false;
        finishedSpawning = false;
    }

    private void Update()
    {
        if (curTimer <= 0 && startSpawning && !finishedSpawning)
        {
            SpawnEnemy();
        }

        if (curTimer > 0) curTimer -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);

        curTimer = timeBetweenSpawns;
        pos++;
        if (pos >= spawnPoints.Length) finishedSpawning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finishedSpawning && !startSpawning)
        {
            startSpawning = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finishedSpawning && !startSpawning)
        {
            startSpawning = true;
        }
    }
}
