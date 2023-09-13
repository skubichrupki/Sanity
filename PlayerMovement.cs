using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // initiate components
    private Rigidbody2D rigidbodyy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // initiate variables
    [SerializeField] private float dirYUpSpeed = 12f;
    [SerializeField] private float dirXMultiplier = 8f;
    [SerializeField] private float dirXSpeed = 0f;

    // STATES START
    // enum = group of values that variable of MovementState data type can hold
    // so: enum -> MovementState is data type, with those values for the variables {value1, value2}
    private enum MovementState { idle, run, jump, fall }
    // values inside curly braces have int ID's. idle = 0 etc


    // This is done to manage state with ints instead of many booleans like IsRunning
    // fe: if State = 0, then idle in Unity

    // Start is called BEFORE the first frame update
    private void Start()
    {
        Debug.Log("Hi mom");
        // get components from Unity
        rigidbodyy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // functions are being executed every single frame
        MovementX();
        MovementY();
        MovementAnimation();
    }

    private void MovementY() 
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Key: W");
            // Vector2(x,y)
            rigidbodyy.velocity = new Vector2(rigidbodyy.velocity.x, dirYUpSpeed);

            // Input.GetKeyDown(KeyCode.W))
            // Input.GetButtonDown("Up")
        }
    }

    private void MovementX() 
    {
        // get -1 or 1 from Input
        dirXSpeed = Input.GetAxisRaw("Horizontal");
        Debug.Log(dirXSpeed);
        // x = -1 or 1 * multiplier 
        rigidbodyy.velocity = new Vector2(dirXSpeed * dirXMultiplier, rigidbodyy.velocity.y);
    }

    private void MovementAnimation() {

        // local state variable of MovementState enum;
        MovementState state;

        // CHECK FOR RUNNING ANIMATION
        // run right
        if (dirXSpeed > 0f)
        {
            // anim.SetBool("IsRunning", true);
            state = MovementState.run;

            spriteRenderer.flipX = false;
            Debug.Log("Run Right");
        }
        // run left
        else if (dirXSpeed < 0f) 
        {
            state = MovementState.run;
            spriteRenderer.flipX = true;
            Debug.Log("Run Left");
        }
        // not running - idle
        else 
        {
            // anim.SetBool("IsRunning", false);
            state = MovementState.idle;
            Debug.Log("idle");
        }

        // CHECK FOR JUMPING ANIMATION
        // if velocity of rigidbody > 0 (0.1) then target force up is applied - jump
        if (rigidbodyy.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }

        if (rigidbodyy.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        // cast state into int with (int)x;
        // after all checks set value of state variable of type MovementState to corresponding int value of enum values
        animator.SetInteger("state", (int)state);
    }


}
