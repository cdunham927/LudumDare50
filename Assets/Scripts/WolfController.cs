using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : EnemyController
{
    public float pushForce;

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
    public override void Idle()
    {
        base.Idle();
    }

    public override void Chase()
    {
        base.Chase();
    }


    public override void Attack()
    {
        bod.AddForce((target.position - transform.position) * pushForce); 
        base.Attack();
    }
}
