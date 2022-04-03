using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numVillagers;
    public float dmg = 10f;

    public int deaths = 0;
    FortuneTeller ft;
    PlayerMovement pMove;
    LivingThing[] livingThings;

    public GameObject gameOverUI;

    public void VillagerDeath()
    {
        numVillagers--;

        if (numVillagers <= 0) Die();
    }

    private void Awake()
    {
        livingThings = FindObjectsOfType<LivingThing>();
        numVillagers = FindObjectsOfType<NPCController>().Length;
        pMove = FindObjectOfType<PlayerMovement>();
        ft = FindObjectOfType<FortuneTeller>();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Pressed L to damage by " + dmg + ".");
                LivingThing[] ob = FindObjectsOfType<LivingThing>();
                foreach (LivingThing d in ob)
                {
                    d.Damage(dmg);
                }
            }
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Die()
    {
        deaths++;
        ft.FTDialogue();

        //NPCs can follow the player
        if (deaths > 0)
        {
            NPCController[] npcs = GameObject.FindObjectsOfType<NPCController>();

            foreach(NPCController n in npcs)
            {
                n.canFollow = true;
            }
        }
        //Players can switch weapons
        if (deaths > 1)
        {
            FindObjectOfType<WeaponRack>().gameObject.SetActive(true);
        }

        Invoke("RespawnThings", 0.5f);
    }

    void RespawnThings()
    {
        foreach (LivingThing l in livingThings)
        {
            l.Respawn();
        }
    }
}
