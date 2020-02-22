using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    public float speed;
    public KeyCode interactKey;

    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
         rigidBody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        rigidBody.velocity = moveDirection * Time.deltaTime * speed;
    }
}
