using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCardController : MonoBehaviour
{
    public float disableTime;

    private void OnEnable()
    {
        Invoke("Disable", disableTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
