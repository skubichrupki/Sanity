using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // initiate components
    private Rigidbody2D rigidbodyy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // initiate variables
    [SerializeField] private float dirYUpSpeed = 15f;
    [SerializeField] private float dirYDownSpeed = 10f;
    [SerializeField] private float dirXMultiplier = 10f;
    [SerializeField] private float dirXSpeed = 0f;
    [SerializeField] private LayerMask groundJump;

    // STATES START
    private enum MovementState { idle, run, jump, fall }
    // enum = group of values that variable of MovementState data type can hold
    // so: enum -> MovementState is data type, with those values for the variables {value1, value2}
    // values inside curly braces have int ID's. idle = 0 etc
    // This is done to manage state with ints instead of many booleans like IsRunning
    // fe: if State = 0, then idle in Unity

    private bool IsGrounded;

    // Start is called BEFORE the first frame update
    private void Start()
    {
        // get components from Unity
        rigidbodyy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();    
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
        // jump up
        if (Input.GetKeyDown(KeyCode.W) && IsGroundedCheck())
        {
            Debug.Log("Jump Up");
            // Vector2(x,y)
            rigidbodyy.velocity = new Vector2(rigidbodyy.velocity.x, dirYUpSpeed);

            // Input.GetKeyDown(KeyCode.W))
            // Input.GetButtonDown("Up")
        }
        // jump down
        else if (Input.GetKeyDown(KeyCode.S) && IsGroundedCheck() == false) 
        {
            Debug.Log("Jump Down");
            rigidbodyy.velocity = new Vector2(rigidbodyy.velocity.x, -dirYDownSpeed);
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

    // check if player is grounded
    private bool IsGroundedCheck()
    {
        // box cast - similar box to box collider, we move it down 0.1f and check if its overlapping with groundJump (ground layer)
        IsGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundJump);

        // returns true if player touches the ground - can jump
        return IsGrounded;
    }


}
