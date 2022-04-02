using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mSpeed;
    
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveDirection.x * mSpeed, moveDirection.y * mSpeed);
        }
    }
}
