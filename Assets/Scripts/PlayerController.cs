using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is just for updating parameters: nothing else
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;
    bool facingRight = true;

    Animator anim;

    //Jump stuff
    bool isGrounded = false;
    [SerializeField]
    private Transform groundCheck;
    float groundRadius = 0.2f;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float jumpForce = 700;

    //Double Jump
    bool doubleJump = false;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", isGrounded);

        if (isGrounded)
            doubleJump = false;

        anim.SetFloat("vSpeed", rb2D.velocity.y);

        if (!isGrounded) return;

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));
        rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);
        if (move > 0 && !facingRight)
            Flip();

        else if (move < 0 && facingRight)
            Flip();
	}

    void Update()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        if ((isGrounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Grounded", false);
            rb2D.AddForce(new Vector2(0, jumpForce));

            if(!doubleJump && !isGrounded)
                doubleJump = true;
           
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
