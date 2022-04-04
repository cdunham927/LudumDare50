using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonSpawnTimer : MonoBehaviour
{
    public Text timerText;
    public float startTime;
    [HideInInspector]
    public float curTime;
    public bool hasSpawned = false;
    GameController cont;
    public bool runTimer = true;
    public DragonController dragon;
    public DragonTimerController dTimer;

    private void Awake()
    {
        curTime = startTime;
        cont = FindObjectOfType<GameController>();
    }

    public void ResetTimer()
    {
        curTime = startTime;
        hasSpawned = false;
        runTimer = true;
    }

    private void Update()
    {
        float minutes = Mathf.FloorToInt(curTime / 60);
        float seconds = Mathf.FloorToInt(curTime % 60);

        if (curTime > 0 && runTimer) curTime -= Time.deltaTime;

        if (!hasSpawned && curTime <= 0)
        {
            dTimer.gameObject.SetActive(true);
            dragon.gameObject.SetActive(true);
            dTimer.EnableTimer();
            hasSpawned = true;
            gameObject.SetActive(false);
        }

        timerText.text = string.Format("Time till dragon spawns\n{0:00}:{1:00}", minutes, seconds);
    }
}
