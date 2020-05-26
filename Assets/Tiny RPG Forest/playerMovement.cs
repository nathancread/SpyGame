using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 
    public Animator animator;
    Vector2 movement;
    // Update is called once per frame
    void Update()
    {
        //handle input
        //left -1 right 1 nothing 0
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement*moveSpeed * Time.fixedDeltaTime);
        //handle movement. Not based on framerate
    }
}
