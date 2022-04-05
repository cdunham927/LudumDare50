using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireTrigger : MonoBehaviour
{
    public float dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("npc"))
        {
            collision.GetComponent<LivingThing>().Damage(dmg);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("npc"))
        {
            collision.GetComponent<LivingThing>().Damage(dmg);
        }
    }
}
