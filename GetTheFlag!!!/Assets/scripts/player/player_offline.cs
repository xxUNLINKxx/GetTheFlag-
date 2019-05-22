using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_offline : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    public float speed;
    public float jumpforce;
    private bool facingRight = true;
    public float groundRadius;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        RaycastHit2D onGround = Physics2D.Raycast(transform.position, -transform.up, groundRadius, whatIsGround);
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            }
        }

        if (facingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!facingRight == true && moveInput > 0)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    void Flip()
    {

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}


