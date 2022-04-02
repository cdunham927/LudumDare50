using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numVillagers;
    public float dmg = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Pressed K to damage by " + dmg + ".");
                LivingThing[] ob = FindObjectsOfType<LivingThing>();
                foreach (LivingThing d in ob)
                {
                    d.Damage(dmg);
                }
            }
        }
    }
}
