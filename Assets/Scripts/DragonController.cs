using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : EnemyController
{
    public GameObject vilTrans;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
    }

    public override void Die()
    {
        base.Die();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void PickAttack()
    {
        base.PickAttack();
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override void Chase()
    {
        Vector2 dir = vilTrans.transform.position - transform.position;

        bod.AddForce(dir * spd * Time.deltaTime);
    }

    public override void AttackTwo()
    {
        base.AttackTwo();
    }

    public override void AttackOne()
    {
        base.AttackOne();
    }
}
