using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    public float speed;
    public KeyCode interactKey;

    private Rigidbody rigidBody;
    private Animator animator;
    
    public int facingDirection = 0;
    public int front = 0, back = 1, left = 2, right = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
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
            animator.SetInteger("facingDirection", left);
        } else if (moveDirection.x > 0) {
            animator.SetInteger("facingDirection", right);
        }

        rigidBody.velocity = moveDirection * Time.deltaTime * speed;

        // print(rigidBody.velocity);
    }
}
