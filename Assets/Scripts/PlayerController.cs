using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using TouchControlsKit;


public class PlayerController : MonoBehaviour
{
    public GameManager GM;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isMoving;
    public float moverInput;

    public bool gameOver;
    public Vector2 move;
    public float xx;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (gameOver == false)
        {
        //    moverInput = Input.GetAxisRaw("Horizontal");
        //    rb.velocity = new Vector2(moverInput * moveSpeed, rb.velocity.y);

            move = TCKInput.GetAxis("Joystick");
           
            rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);
           
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            //isMoving = (Input.GetAxisRaw("Horizontal") != 0);
            isMoving = (Mathf.Round(move.x) != 0);
           
            //isMoving = (Input.GetAxisRaw("Horizontal") != 0);

            anim.SetBool("isMoving", isMoving);

            if (Input.GetButtonDown("Jump") && isGrounded || TCKInput.GetAction("JumpButton", EActionEvent.Down) && isGrounded)
            {
                anim.Play("jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (moverInput > 0 || Mathf.Round(move.x)>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moverInput < 0|| Mathf.Round(move.x) <0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void MobileInput()
    {

        //if (TCKInput.GetAction("JumpButton", EActionEvent.Down))
        //{
        //    Jumping();
        //}

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="obstacle")
        {
            print("J'ai cogné un obstacle !");
            GM.GameOver();
            gameOver = true;
            //anim.Play("jump");
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<PlayerController>().enabled = false;
        }

        if (collision.gameObject.tag == "coin")
        {
            GM.Coins += 1;
            GM.UpdateCoin(); 

            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);

            print("J'ai touché une pièce !");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish" && GM.Coins > 0)
        {
            SceneManager.LoadScene(GM.StageSuivant);
        }
    }
}
