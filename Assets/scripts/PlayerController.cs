using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;

    // Start is called before the first frame update
    void Start()
    {
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("deadarea"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("lvl2");
        }
        if (collision.gameObject.CompareTag("Finish2"))
        {
            SceneManager.LoadScene("lvl3");
        }

    }

}