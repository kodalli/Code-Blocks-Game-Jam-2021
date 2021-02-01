using System.Collections;
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

    //Particle System
    private ParticleSystem dust;

    void createDust()
    {
        dust.Play();
    }
    void Flip()
    {
        createDust();
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = LevelManager.instance.ps;
        dust = GameObject.Find("dust").GetComponent<ParticleSystem>();

    }
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // animate player
        anim.SetFloat("AnimSpeed", movement.sqrMagnitude);

        // make character face correction direction
        if (movement.x > 0 && !facingRight || movement.x < 0 && facingRight)
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

}
