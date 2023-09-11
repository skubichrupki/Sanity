using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // initiate components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRend;

    // initiate variables
    [SerializeField] private float dirYUpSpeed = 12f;
    [SerializeField] private float dirXMultiplier = 8f;
    [SerializeField] private float dirXSpeed = 0f;

    // Start is called BEFORE the first frame update
    private void Start()
    {
        Debug.Log("Hi mom");
        // get components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MovementX();
        MovementXAnim();
        Jump();
    }

    private void Jump() 
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Key: W");
            // Vector2(x,y)
            rb.velocity = new Vector2(rb.velocity.x, dirYUpSpeed);

            // Input.GetKeyDown(KeyCode.W))
            // Input.GetButtonDown("Up")
        }
    }

    private void MovementX() 
    {
        // get -1 or 1 from Input
        dirXSpeed = Input.GetAxisRaw("Horizontal");
        // x = -1 or 1 * multiplier 
        rb.velocity = new Vector2(dirXSpeed * dirXMultiplier, rb.velocity.y);
    }

    private void MovementXAnim() {
        // run right
        if (dirXSpeed > 0f)
        {
            anim.SetBool("IsRunning", true);
            spriteRend.flipX = false;
        }
        // run left
        else if (dirXSpeed < 0f) 
        {
            anim.SetBool("IsRunning", true);
            spriteRend.flipX = true;
        }
        // not running
        else 
        {
            anim.SetBool("IsRunning", false);
        }
    }


}
