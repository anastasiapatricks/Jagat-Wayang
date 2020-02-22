using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    public float speed;
    public KeyCode interactKey;

    private Rigidbody rigidBody;
    private Animator animator;

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

        
        //if (moveDirection != Vector3.zero) {
            rigidBody.velocity = moveDirection * Time.deltaTime * speed;
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.z);
        
    }
}
