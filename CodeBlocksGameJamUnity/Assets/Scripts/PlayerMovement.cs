using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    Vector2 mousePos;
    Vector2 movement;
    bool facingRight = false;
    Animator anim;
    private Camera cam;
    [SerializeField] private bool gravityOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 dir = mousePos - rb.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // animate player
        anim.SetFloat("AnimSpeed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        // make character face correction direction
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
}
