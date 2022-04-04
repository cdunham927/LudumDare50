using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonTimerController : MonoBehaviour
{
    float dis;
    public DragonController dragon;
    public Transform vilTrans;
    public Text distanceText;

    void Awake()
    {
        dragon = FindObjectOfType<DragonController>();
    }

    public void EnableTimer()
    {
        distanceText.gameObject.SetActive(true);
    }

    private void Update()
    {
        dis = Vector2.Distance(dragon.transform.position, vilTrans.position);

        float d = Mathf.Round((dis * 100f) * 0.01f);
        distanceText.text = "Dragon imminent: " + d.ToString() + "m";
    }
}
