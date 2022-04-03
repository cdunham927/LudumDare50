using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixThingController : MonoBehaviour
{

    public float timeToInteract = 0.5f;
    float curTime;
    public bool inRange = false;
    PlayerMovement pMove;
    public bool canFix = false;

    private void Awake()
    {
        pMove = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (inRange && Input.GetKey(KeyCode.E))
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
        Debug.Log("Interacting");
        curTime = 0f;
        pMove.SwitchWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
