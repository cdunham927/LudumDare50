using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamDetectorController : MonoBehaviour
{
    public float timeToInteract = 0.5f;
    float curTime;
    public bool inRange = false;
    PlayerMovement pMove;
    public bool canFix = false;

    public FloodController parent;

    private void Awake()
    {
        pMove = FindObjectOfType<PlayerMovement>();
        parent = GetComponentInParent<FloodController>();
    }

    private void Update()
    {
        if (parent.inRange && Input.GetKey(KeyCode.E))
        {
            curTime += Time.deltaTime;
        }
        else curTime = 0f;

        if (curTime > timeToInteract && canFix)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        //Stop timer
        Debug.Log("Interacting");
        parent.runTimer = false;
        curTime = 0f;
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
