using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    PlayerMovement move;


    private void Awake()
    {
        move = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (move.canMove)
        {
            // convert mouse position into world coordinates
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get direction you want to point at
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

            // set vector of transform directly
            transform.up = direction;
        }
    }
}
