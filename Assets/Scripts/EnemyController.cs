using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : LivingThing
{
    public enum enemystates { idle, chase, attack }
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

    void Chase()
    {

    }

    void PickAttack()
    {

    }

    public void AttackOne()
    {

    }

    public void AttackTwo()
    {

    }

    private void Update()
    {
        switch (curState)
        {
            case (enemystates.idle):
                Idle();
                break;
            case (enemystates.chase):
                Chase();
                break;
            case (enemystates.attack):
                PickAttack();
                break;
        }
    }
}
