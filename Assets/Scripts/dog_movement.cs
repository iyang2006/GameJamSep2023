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
    private bool windUp;
    [SerializeField] private float poundDelay;
    [SerializeField] private float maxDelay;
    private float poundTime;
    private bool delayed;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        inPound = false;
        windUp = false;
        poundTime = 0;
        delayed = false;
    }

    private LayerMask mask = LayerMask.GetMask("dog");
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "platform_tag") {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("dog grounded");
                inPound = false;
                poundTime = 0;
                delayed = false;
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "platform_tag") {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        float speed = moveSpeed;
        
        if ((Input.GetKey(KeyCode.S) == false) && inPound && (delayed == false) && (Time.time - poundTime >= poundDelay)) {
            delayed = true;
            windUp = false;
        }

        if (inPound) {
            if (windUp) {
                speed = (float)(0.3 * moveSpeed);
                Vector3 vel = body.velocity;
                vel.y = (float)(vel.y * 0.5);
                movement.y = 1;
                body.velocity = vel;
            }
            else {
                movement.y = 0;
                Vector3 vel = body.velocity;
                vel.y = -10 * poundStrength;
                body.velocity = vel;
            }
        }
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

        if (inPound && (Time.time - poundTime >= maxDelay)) {
            delayed = true;
            windUp = false;
        }

        //ground pound
        if (!(inPound && delayed)) {
            if (Input.GetKey(KeyCode.S) && grounded == false) {
                if (inPound == false) {
                    Debug.Log("initiate pounding");
                    poundTime = Time.time;
                }
                inPound = true;
                windUp = true;
            }
        }

        body.MovePosition(body.position + (movement * speed * Time.fixedDeltaTime));
    }
}
