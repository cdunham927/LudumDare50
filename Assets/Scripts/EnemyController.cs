using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : LivingThing
{
    public enum enemystates { idle, follow, panic }
    public enemystates curState = enemystates.idle;

    public void ChangeState(enemystates newState)
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

    }

    void Panic()
    {

    }

    private void Update()
    {
        switch (curState)
        {
            case (enemystates.idle):
                Idle();
                break;
            case (enemystates.follow):
                Follow();
                break;
            case (enemystates.panic):
                Panic();
                break;
        }
    }
}
