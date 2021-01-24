using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    private bool facingRight = false;
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 movement;
    private Vector2 player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        movement = (player - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement.x > 0 && !facingRight)
            Flip();
        else if (movement.x < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Die()
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
            Die();
    }


}
