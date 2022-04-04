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

    private void Update()
    {
        dis = Vector2.Distance(dragon.transform.position, vilTrans.position);

        distanceText.text = "Dragon imminent: " + dis.ToString() + "m";
    }
}
