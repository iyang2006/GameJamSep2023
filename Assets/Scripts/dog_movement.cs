using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private BoxCollider collid;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float poundStrength;
    private Vector3 movement;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "floor_tag") {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "floor_tag") {
            grounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //horizontal movement
        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow)) {
            movement.x = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            movement.x = -1;
        }
        else {
            movement.x = 1;
        }

        //jumping
        if (Input.GetKey(KeyCode.UpArrow) && grounded == true) {
            Debug.Log("Jump");
            Vector3 vel = body.velocity;
            vel.y = 5 * jumpStrength;
            body.velocity = vel;
        }

        //ground pound
        if (Input.GetKey(KeyCode.DownArrow) && grounded == false) {
            Debug.Log("Pound");
            Vector3 vel = body.velocity;
            vel.y = -10 * poundStrength;
            body.velocity = vel;
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }
}