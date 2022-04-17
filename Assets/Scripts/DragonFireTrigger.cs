using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireTrigger : MonoBehaviour
{
    public float dmg;
    public float cooldown;
    float curCools;

    private void Update()
    {
        if (curCools > 0) curCools -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("npc")) && curCools <= 0)
        {
            collision.GetComponent<LivingThing>().Damage(dmg);
            curCools = cooldown;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("npc")) && curCools <= 0)
        {
            collision.GetComponent<LivingThing>().Damage(dmg);
            curCools = cooldown;
        }
    }
}
