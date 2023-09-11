using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // initiate components
    private Rigidbody2D rb;
    private Animator anim;

    // initiate variables
    private float dirYUp = 12f;
    private float dirXMultiplier = 8f;
    private float dirX = 0f;

    // Start is called BEFORE the first frame update
    private void Start()
    {
        Debug.Log("Hi mom");
        // get components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                    rb.velocity = new Vector2(rb.velocity.x, dirYUp);

                    // Input.GetKeyDown(KeyCode.W))
                    // Input.GetButtonDown("Up")
                }
    }

    private void MovementX() 
    {
        // get -1 or 1 from Input
        dirX = Input.GetAxisRaw("Horizontal");
        // x = -1 or 1 * multiplier 
        rb.velocity = new Vector2(dirX * dirXMultiplier, rb.velocity.y);
    }


    private void MovementXAnim() {
        // run right
        if (dirX > 0f)
        {
            anim.SetBool("IsRunning", true);
        }
        // run left
        else if (dirX < 0f) 
        {
            anim.SetBool("IsRunning", true);
        }
        // not running
        else 
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
