using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCMB : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attPos;
    public float attRange;
    public LayerMask whatIsEnemies;
    public int dmg;

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attPos.position, attRange, whatIsEnemies );
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<LivingThing>().Damage(dmg);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        void OnDrawGizmoSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attPos.position, attRange);
        }
    }
}
