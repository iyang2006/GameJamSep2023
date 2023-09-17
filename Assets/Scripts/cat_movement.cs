using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private float rayLength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float poundIntensity;
    private Vector3 movement;
    [SerializeField] private bool grounded;
    private int jumpCount;
    [SerializeField] private bool onWall;
    public bool leftWall;
    [SerializeField] private float wallJumpStrength;
    private bool inJump;
    private float jumpTime;
    private bool bounce;
    [SerializeField] private float jumpTimeLimit;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource catSound;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumpCount = 0;
        onWall = false;
        leftWall = true;
        inJump = true;
        bounce = false;
    }

    private LayerMask mask = LayerMask.GetMask("cat");

    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("cat grounded");
                grounded = true;
                jumpCount = 0;
            }
            else if (Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask)) {
                onWall = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
            }
            else if (Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask)) {
                onWall = false;
                bounce = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        int groundMask = 1 << LayerMask.NameToLayer("platforms");
        int boxMask = 1 << LayerMask.NameToLayer("boxes");

        if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, groundMask)) {
            grounded = true;
        }
        else {
            grounded = false;
        }

        if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, boxMask)) {
            grounded = true;
        }


        if (!(Physics.Raycast(trans.position, (trans.right), rayLength, groundMask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, groundMask))) {
            onWall = false;
            bounce = false;
        }
        else {
            onWall = true;
        }

        if ((Physics.Raycast(trans.position, (trans.right), rayLength, boxMask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, boxMask)))
        {
            onWall = true;
        }

        if (Time.time - jumpTime >= jumpTimeLimit) {
            jumpTime = 0;
            inJump = false;
        }

        //horizontal movement
        if (inJump == false) {
            if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow)) {
                movement.x = 0;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                movement.x = -1;
                leftWall = true;
            }
            else {
                movement.x = 1;
                leftWall = false;
            }
        }

        //jumping
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (grounded == true) {
                grounded = false;
                Debug.Log("cat jump");
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
                body.velocity = vel;
                jumpSound.Play();
            }
            else if (onWall == true && jumpCount < 3) {
                inJump = true;
                movement.x = 0;
                jumpTime = Time.time;
                onWall = false;
                Debug.Log("wall jump");
                if (bounce == false) {
                    jumpCount += 1;
                }
                bounce = true;
                Debug.Log(jumpCount);
                Vector3 vel = body.velocity;
                vel.y = 5 * wallJumpStrength;
                if (leftWall == true) {
                    vel.x = 2 * jumpStrength;
                }
                else {
                    vel.x = -2 * jumpStrength;
                }
                body.velocity = vel;
                catSound.pitch = (4 - jumpCount);
                catSound.Play();
                catSound.pitch = (4 - jumpCount);
            }
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.deltaTime));

    }

    public void Bomb(float intensity) {
        if (grounded) {
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = poundIntensity * intensity;
            body.velocity = vel;
        }
    }
}
