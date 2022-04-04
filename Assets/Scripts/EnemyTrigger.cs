using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public EnemyController enemy;
    public bool finishedSpawning;

    public int enemyCount;

    private void Awake()
    {
        enemyCount = spawnPoints.Length;
    }

    

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemy, spawnPoints[i].transform.position, Quaternion.identity);
        }

        finishedSpawning = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finishedSpawning)
        {
            SpawnEnemies();
        }
    }
}
