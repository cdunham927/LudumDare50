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
    //public Animator camAnim;
    //public Animator playerAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && timeBtwAttack <= 0)
        {
            Debug.Log("Attacking");
            //camAnim.SetTrigger("Shake");
            //playerAnim.SetTrigger("attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attPos.position, attRange, whatIsEnemies);
            if (enemiesToDamage.Length > 0)
            {
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().Damage(dmg);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }

        if (timeBtwAttack > 0) timeBtwAttack -= Time.deltaTime;

    }
    void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attPos.position, attRange);
    }
}
