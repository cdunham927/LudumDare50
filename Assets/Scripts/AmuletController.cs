using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletController : MonoBehaviour
{
    public float timeToAdd;
    DragonSpawnTimer dTimer;

    private void Awake()
    {
        dTimer = FindObjectOfType<DragonSpawnTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup();
        }
    }

    public void Pickup()
    {
        dTimer.curTime += timeToAdd;
        gameObject.SetActive(false);
    }
}
