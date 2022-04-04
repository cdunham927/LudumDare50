using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : LivingThing
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public enum enemystates { idle, chase, attack }
    public enemystates curState = enemystates.idle;
    Transform target;
    protected Rigidbody2D bod;
    protected Animator anim;

    public bool inRange = false;
    public float timeBetweenTargetFinds;
    public float checkRadius = 0.5f;
    public LayerMask targMask;
    public CircleCollider2D detCirc;
    float dis;
    public float idleRange;
    public float maxFollowDistance;
    PlayerMovement pCont;
    
    public float attackRange;
    public int dmg;
    public Transform attPos;

    public override void Awake()
    {
        base.Awake();
        pCont = FindObjectOfType<PlayerMovement>();
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void ChangeState(enemystates newState)
    {
        curState = newState;
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
        target = pCont.transform;
    }

    //Pick a random target in range
    public void GetTarget()
    {
        Collider2D[] thingsInRange = Physics2D.OverlapCircleAll(transform.position, detCirc.radius + checkRadius, targMask);
        target = thingsInRange[Random.Range(0, thingsInRange.Length)].transform;

        ChangeState(enemystates.chase);
    }

    public override void Die()
    {
        base.Die();
    }

    public virtual void Idle()
    {
        //Need the enemies to change state when NPCs are in range too
        if (inRange) GetTarget();
    }

    public virtual void Chase()
    {
        if (target != null)
        {
            anim.SetFloat("moveX", bod.velocity.x);
            anim.SetFloat("moveY", bod.velocity.y);

            dis = Vector2.Distance(transform.position, target.position);

            if (dis >= idleRange) ChangeState(enemystates.idle);

            if (target != null && dis > maxFollowDistance)
            {
                Vector2 dir = target.position - transform.position;
                bod.AddForce(dir * spd * Time.deltaTime);
            }

            if (dis < attackRange) Attack();
        }
    }

    public virtual void Attack()
    {
        { 
            Debug.Log("Attacking");
            //enemyAnim.SetTrigger("attack")
            Collider2D[] NPCtodmg = Physics2D.OverlapCircleAll(attPos.position, attackRange, targMask);
            if (NPCtodmg.Length > 0)
            {
                for (int i = 0; i < NPCtodmg.Length; i++)
                {
                    NPCtodmg[i].GetComponent<LivingThing>().Damage(dmg);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }

        if (timeBtwAttack > 0) timeBtwAttack -= Time.deltaTime;

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
                Attack();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("npc") || collision.gameObject.CompareTag("Player"))
        {
            target = collision.transform;
        }

    }
    void OnDrawGizmo()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attPos.position, attackRange);
    }
}