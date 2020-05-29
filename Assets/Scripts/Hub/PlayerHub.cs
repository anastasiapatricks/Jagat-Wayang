using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    public float speed;
    public KeyCode interactKey;
    public KeyCode jumpKey;
    public bool isStanding = true;

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
        Physics.gravity = new Vector3(0, -75f, 0);

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

        if (Input.GetButtonDown("Jump") && isStanding) {
            rigidBody.AddForce(new Vector3(0, 250, 0), ForceMode.Impulse);
            isStanding = false;
        }

        // print(rigidBody.velocity);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
            {
                isStanding = true;
            }
    }
}


