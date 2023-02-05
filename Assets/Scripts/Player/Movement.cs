using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool isJumping;
    public SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        //Move left and right. D for left and A for right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 10 * Time.deltaTime);
            transform.localScale = new Vector2(5, 5);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * 10 * Time.deltaTime);
            transform.localScale = new Vector2(-5, 5);
        }

        //Jumping System. Makes it so that you can only jump when you're colliding with the ground.

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector2.up * 10;
            isJumping = true;
        }

        // E to Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && transform.localScale == new Vector3(-5, 5, 0))
        {
            rb.velocity = Vector2.left * 15;


        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && transform.localScale == new Vector3(5, 5, 0))
        {
            rb.velocity = Vector2.right * 15;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

        }


    }
}














