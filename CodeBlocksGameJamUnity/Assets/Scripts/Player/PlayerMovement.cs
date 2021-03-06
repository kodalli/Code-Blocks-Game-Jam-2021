﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    Vector2 movement;
    bool facingRight = false;
    Animator anim;
    PlayerState ps;
    //private Camera cam;
    //[SerializeField] private bool gravityOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = LevelManager.instance.ps;
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Vector2 dir = mousePos - rb.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;

        // animate player
        anim.SetFloat("AnimSpeed", movement.sqrMagnitude);

        // make character face correction direction
        if (movement.x > 0 && !facingRight)
            Flip();
        else if (movement.x < 0 && facingRight)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            if (ps.HP > 10)
                ps.HP -= 10;
            else
                LevelManager.instance.Respawn();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
