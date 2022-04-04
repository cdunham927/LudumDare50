using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LivingThing
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public bool canMove = true;

    public int curWeapon = 0;
    public GameObject[] weapons;

    private Animator animator;

    SpriteRenderer rend;
    public SpriteRenderer[] weaponRend;

    public override void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        base.Awake();
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
    }

    public override void Die()
    {
        FindObjectOfType<GameController>().Die();
        base.Die();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public void SwitchWeapon()
    {
        if (curWeapon == weapons.Length - 1)
        {
            weapons[curWeapon].SetActive(false);
            curWeapon = 0;
            weapons[curWeapon].SetActive(true);
        }
        else
        {
            weapons[curWeapon].SetActive(false);
            curWeapon++;
            weapons[curWeapon].SetActive(true);
        }
    }

    void Update()
    {
        ProcessInputs();
       
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Vector2 mousePos = Camera.main.
        if (mousePos.y > 0.5f)
        {
            rend.sortingOrder = 3;
            //foreach (SpriteRenderer r in weaponRend)
            //r.sortingOrder = 1;
        }
        else
        {
            rend.sortingOrder = 1;
            //foreach (SpriteRenderer r in weaponRend)
                //r.sortingOrder = 0;
        }
            Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        Move();
        animator.SetFloat("movementX", moveX);
        animator.SetFloat("movementY", moveY);
            
        
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveDirection.x * spd, moveDirection.y * spd);
        }
    }
}
