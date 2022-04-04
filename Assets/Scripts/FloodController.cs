using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodController : MonoBehaviour
{
    public bool inRange;
    public Text timerText;
    public float startTime;
    float curTime;
    public bool hasFlooded = false;
    public string gameOverMessage;
    GameController cont;
    public bool runTimer = true;

    private void Awake()
    {
        curTime = startTime;
        cont = FindObjectOfType<GameController>();
    }

    public void ResetTimer()
    {
        timerText.gameObject.SetActive(true);
        curTime = startTime;
        hasFlooded = false;
        runTimer = true;
    }

    private void Update()
    {
        float minutes = Mathf.FloorToInt(curTime / 60);
        float seconds = Mathf.FloorToInt(curTime % 60);

        if (curTime > 0 && runTimer) curTime -= Time.deltaTime;

        if (!hasFlooded && curTime <= 0)
        {
            cont.SetGameOverText(gameOverMessage);
            cont.GameOver();
            hasFlooded = true;
        }

        timerText.text = string.Format("Time till flood\n{0:00}:{1:00}", minutes, seconds);
    }
}
