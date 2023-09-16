using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private float rayLength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float poundStrength;
    private Vector3 movement;
    private bool grounded;
    private bool inPound;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        inPound = false;
    }

    private LayerMask mask = LayerMask.GetMask("dog");
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "platform_tag") {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("dog grounded");
                inPound = false;
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "platform_tag") {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                grounded = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //horizontal movement
        if (Input.GetKey(KeyCode.A) == Input.GetKey(KeyCode.D)) {
            movement.x = 0;
        }
        else if (Input.GetKey(KeyCode.A)) {
            movement.x = -1;
        }
        else {
            movement.x = 1;
        }

        //jumping
        if (Input.GetKey(KeyCode.W) && grounded == true) {
            Debug.Log("dog jump");
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = 5 * jumpStrength;
            body.velocity = vel;
        }

        //ground pound
        if (Input.GetKey(KeyCode.S) && grounded == false) {
            Debug.Log("pound");
            inPound = true;
            Vector3 vel = body.velocity;
            vel.y = -10 * poundStrength;
            body.velocity = vel;
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }
}
