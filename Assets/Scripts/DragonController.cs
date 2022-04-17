using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : EnemyController
{
    public Transform vilTrans;
    public float rotSpd;
    NPCController[] villagers;
    int curVillager = 0;

    public float slowSpd;
    public float fastSpd;

    public override void Awake()
    {
        villagers = FindObjectsOfType<NPCController>();
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
        curVillager = Random.Range(0, villagers.Length);
        //InvokeRepeating("GetNewNPCTarget", 0.01f, 10f);
        GetNewNPCTarget();
        base.OnEnable();
    }

    void GetNewNPCTarget()
    {
        target = villagers[curVillager].transform;
        if (curVillager >= villagers.Length - 1)
        {
            curVillager = 0;
        }
        else curVillager++;
        //target = villagers[Random.Range(0, villagers.Length)].transform;
    }

    public override void Attack()
    {

    }

    public override void Idle()
    {
        base.Idle();
        ChangeState(enemystates.chase);
    }

    public override void Chase()
    {
        if (!target.gameObject.activeInHierarchy) GetNewNPCTarget();

        float dis = Vector2.Distance(transform.position, target.transform.position);
        if (dis < 15f) spd = slowSpd;
        else spd = fastSpd;

        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpd * Time.deltaTime);

        //Vector2 dir = vilTrans.transform.position - transform.position;

        bod.AddForce(transform.up * spd * Time.deltaTime);
    }
}
