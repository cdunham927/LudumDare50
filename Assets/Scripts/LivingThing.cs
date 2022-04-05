using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingThing : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float def;
    public float spd;
    public Vector3 spawnPoint;
    public bool hasSetSpawn = false;

    public virtual void Awake()
    {
        spawnPoint = transform.position;
        hasSetSpawn = true;
    }

    public virtual void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = spawnPoint;
        hp = maxHp;
    }

    public virtual void OnEnable()
    {
        hp = maxHp;
    }

    public virtual void Damage(float amt)
    {
        float totDmg = amt - def;
        if (totDmg <= 0) totDmg = 1;

        hp -= totDmg;

        if (hp <= 0) Die();
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
