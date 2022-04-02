using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetectionController : MonoBehaviour
{
    public NPCController parent;

    private void Awake()
    {
        parent = GetComponentInParent<NPCController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parent.inRange = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parent.inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parent.inRange = false;
        }
    }
}
