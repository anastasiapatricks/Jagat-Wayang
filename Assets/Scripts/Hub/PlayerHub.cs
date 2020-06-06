using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    public float speed;
    public float jumpImpulse;

    private bool isGrounded = true;

    private Rigidbody rigidBody;
    private SpriteRenderer sprite;
    private Animator animator;
    
    private int front = 0, back = 1, left = 2, right = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        animator.SetBool("isWalking", moveDirection != Vector3.zero);

        if (moveDirection.z > 0) {
            animator.SetInteger("facingDirection", back);
        } else if (moveDirection.z < 0) {
            animator.SetInteger("facingDirection", front);
        }

        if (moveDirection.x < 0) {
            //animator.SetInteger("facingDirection", left);
            sprite.flipX = false;
        } else if (moveDirection.x > 0) {
            //animator.SetInteger("facingDirection", right);
            sprite.flipX = true;
        }

        rigidBody.velocity = moveDirection * Time.deltaTime * speed;

        if (isGrounded && Input.GetButtonDown("Jump")) {
            rigidBody.AddForce(jumpImpulse * Vector3.up, ForceMode.VelocityChange);
            isGrounded = false;
        }

        // print(rigidBody.velocity);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            isGrounded = true;
        }
    }
}


