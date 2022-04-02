using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Vector3 worldPosition;
    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        aimTransform.LookAt(mousePos);
    }
}
