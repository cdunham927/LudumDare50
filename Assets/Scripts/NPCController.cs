using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : LivingThing
{
    public enum npcstates { idle, follow, panic }
    public npcstates curState = npcstates.idle;
    public bool inRange = false;
    Rigidbody2D bod;
    //PlayerController player;
    Transform target;
    public float followDistance = 3f;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bod = GetComponent<Rigidbody2D>();
    }

    public void ChangeState(npcstates newState)
    {
        curState = newState;
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
    }

    public override void Die()
    {
        base.Die();
    }

    void Idle()
    {

    }

    void Follow()
    {
        if (target != null)
        {
            Vector2 dir = target.position - transform.position;
            bod.AddForce(dir * spd * Time.deltaTime);
        }

        if (!inRange) ChangeState(npcstates.idle);
    }

    void Panic()
    {

    }

    private void Update()
    {
        switch(curState)
        {
            case (npcstates.idle):
                Idle();
                break;
            case (npcstates.follow):
                Follow();
                break;
            case (npcstates.panic):
                Panic();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            ChangeState(npcstates.follow);
        }
    }
}
