using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // component - RigidBody2D
    private Rigidbody2D rb;

    private float dirYUp = 12f;
    private float dirXMultiplier = 8f;

    // Start is called BEFORE the first frame update
    private void Start()
    {
        Debug.Log("Hi mom");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // LEFT RIGHT MOVEMENT STARTS HERE --
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * dirXMultiplier, rb.velocity.y);
        // LEFT RIGHT MOVEMENT ENDS HERE --


        // JUMP STARTS HERE --
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W was pressed");
            // Vector2(x,y)
            rb.velocity = new Vector2(rb.velocity.x, dirYUp);
            // Edit -> Project Settings -> Input Manager instead of hardcoding keys

            // Input.GetKeyDown(KeyCode.W))
            // Input.GetButtonDown("Up")
        }
        // JUMP ENDS HERE --

        // if rb.velocity y > 0 then you CANT press W anymore and CAN press S



    }
}
