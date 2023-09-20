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
    [SerializeField] private float dirXMultiplier = 11f;
    [SerializeField] private float dirXSpeed = 0f;
    [SerializeField] private float dirXSlideSpeed = 15f;
    [SerializeField] private float slideDuration = 0.3f;

    [SerializeField] private LayerMask groundJump;

    private bool isSliding = false;
    private bool isSlidingRight = false;
    private bool isSlidingLeft = false;
    private bool IsGrounded = false;

    // STATES START - idle 0, run 1, jump 2, fall 3, slide 4
    private enum MovementState { idle, run, jump, fall, slide }
    // enum = group of values that variable of MovementState data type can hold
    // so: enum -> MovementState is data type, with those values for the variables {value1, value2}
    // values inside curly braces have int ID's. idle = 0 etc
    // This is done to manage state with ints instead of many booleans like IsRunning
    // fe: if State = 0, then idle in Unity

    // ----------------------------------------------------------------------------------------

    // Start is called BEFORE the first frame update
    private void Start()
    {
        // get components from Unity
        rigidbodyy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    // ----------------------------------------------------------------------------------------

    private void Update()
    {
        MovementX();
        MovementY();
        MovementAnimation();
    }

    // ----------------------------------------------------------------------------------------
    // MOVEMENT UP - DOWN
    private void MovementY() 
    {
        // JUMP - GO UP
        if (Input.GetKeyDown(KeyCode.W) && IsGroundedCheck() && !isSliding)
        {
            // Vector2(x,y)
            rigidbodyy.velocity = new Vector2(rigidbodyy.velocity.x, dirYUpSpeed);

            // Input.GetKeyDown(KeyCode.W))
            // Input.GetButtonDown("Up")
        }
        // jump down
        else if (Input.GetKeyDown(KeyCode.S) && IsGroundedCheck() == false) 
        {
            rigidbodyy.velocity = new Vector2(rigidbodyy.velocity.x, -dirYDownSpeed);
        }
    }
    // ----------------------------------------------------------------------------------------
    // MOVEMENT LEFT - RIGHT
    private void MovementX() 
    {
        // regular movement
        // get -1 or 1 from Input
        dirXSpeed = Input.GetAxisRaw("Horizontal");
        // x = -1 or 1 * multiplier 
        rigidbodyy.velocity = new Vector2(dirXSpeed * dirXMultiplier, rigidbodyy.velocity.y);

        // SLIDE CHECK
        if (Input.GetKeyDown(KeyCode.S) && IsGroundedCheck() && (dirXSpeed == 1 || dirXSpeed == -1) && !isSliding)
        {
            // start Coroutine made from Ienumerator
            StartCoroutine(StartSlide());

            if (dirXSpeed == 1)
            {
                isSlidingRight = true;
            }
            else if (dirXSpeed == -1)
            {
                isSlidingLeft = true;
            }

        }

        // SLIDE TIMER
        IEnumerator StartSlide()
        {
            isSliding = true;

            // wait for 1 second
            yield return new WaitForSeconds(slideDuration);

            isSliding = false;
            isSlidingRight = false;
            isSlidingLeft = false;

        }

        // SLIDE RIGHT VELOCITY
        if (isSlidingRight)
        {
            rigidbodyy.velocity = new Vector2(dirXSlideSpeed, rigidbodyy.velocity.y);

        }
        // SLIDE LEFT VELOCITY
        else if (isSlidingLeft)
        {
            rigidbodyy.velocity = new Vector2(-dirXSlideSpeed, rigidbodyy.velocity.y);
        }

    }

    // ----------------------------------------------------------------------------------------
    // ANIMATIONS BY STATE
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
        }
        // run left
        else if (dirXSpeed < 0f) 
        {
            state = MovementState.run;
            spriteRenderer.flipX = true;
        }
        // not running - idle
        else 
        {
            // anim.SetBool("IsRunning", false);
            state = MovementState.idle;
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

        if (isSliding)
        {
            state = MovementState.slide;
        }

        // cast state into int with (int)x;
        // after all checks set value of state variable of type MovementState to corresponding int value of enum values
        animator.SetInteger("state", (int)state);
    }


    // ----------------------------------------------------------------------------------------
    // GROUND CHECK
    private bool IsGroundedCheck()
    {
        // box cast - similar box to box collider, we move it down 0.1f and check if its overlapping with groundJump (ground layer)
        IsGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundJump);

        // returns true if player touches the ground - can jump when true
        return IsGrounded;
    }


}
