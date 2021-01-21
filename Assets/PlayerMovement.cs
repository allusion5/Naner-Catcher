using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D catcher;
    private SpriteRenderer facing;
    private AudioSource scream;
    public GameObject pickupGlow;
    private float moveInput;
    public Animator animator;
    public int score;
    public bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        catcher = GetComponent<Rigidbody2D>();
        facing = GetComponent<SpriteRenderer>();
        scream = GetComponent<AudioSource>();
        
    }
    void SetFacing()
    {

        if (catcher.velocity.x > 0)
        {
            facing.flipX = true;
        }

        if (catcher.velocity.x < 0)
        {
            facing.flipX = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        gameActive = GameObject.Find("gameManager").GetComponent<gameManager>().gameActive;

        if (gameActive == true)
        {
            SetFacing();

            moveInput = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveInput, 0);
            animator.SetFloat("Speed", Mathf.Abs(catcher.velocity.x));
            catcher.AddForce(movement * speed);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            catcher.velocity = Vector2.zero;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            pickupGlow.gameObject.SetActive(true);
            score++;
            scream.Play();
        }
    }
void OnCollisionStay2D(Collision2D collision)
    {

    }
}
