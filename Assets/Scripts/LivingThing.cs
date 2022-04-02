using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingThing
{
    public float maxHp;
    public float hp;
    public float def;
    public float spd;

    public void Damage(float amt) { }
    public void Die() { }
}
