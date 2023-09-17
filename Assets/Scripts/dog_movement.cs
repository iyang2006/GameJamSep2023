using JetBrains.Annotations;
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
    public bool leftWall;
    [SerializeField] private float poundRad;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dogSound;
    [SerializeField] private AudioSource poundSound;
    [SerializeField] private AudioSource rarePoundSound;
    [SerializeField] private float rarePoundSoundIntensity;


    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        inPound = false;
        windUp = false;
        poundTime = 0;
        delayed = false;
        leftWall = true;
    }

    private LayerMask mask = LayerMask.GetMask("dog");
    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("dog grounded");
                if (inPound) {
                    Debug.Log("Pound Intensity: " + (Time.time - poundTime));
                    if (Time.time - poundTime > rarePoundSoundIntensity)
                    {
                        rarePoundSound.Play();
                    } else
                    {
                        poundSound.Play();
                    }

                    BombThem((Time.time - poundTime));
                    mainCamera.GetComponent<camera_controller>().shakeScreen((Time.time - poundTime));
                }
                inPound = false;
                poundTime = 0;
                delayed = false;
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
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

        float speed = moveSpeed;
        
        if ((Input.GetKey(KeyCode.S) == false) && inPound && (delayed == false) && (Time.time - poundTime >= poundDelay)) {
            delayed = true;
            windUp = false;
        }

        Vector3 tempV = body.velocity;
        tempV.x = 0;
        tempV.z = 0;
        body.velocity = tempV;


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
            leftWall = true;
        }
        else {
            movement.x = 1;
            leftWall = false;
        }

        //jumping
        if (Input.GetKey(KeyCode.W) && grounded == true) {
            Debug.Log("dog jump");
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = 5 * jumpStrength;
            body.velocity = vel;
            jumpSound.Play();
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
                    dogSound.Play();
                    poundTime = Time.time;
                }
                inPound = true;
                windUp = true;
            }
        }

        body.MovePosition(body.position + (movement * speed * Time.deltaTime));
    }

    private void BombThem(float intensity) {
        Collider[] colliders = Physics.OverlapSphere(trans.position, poundRad);
        foreach (Collider c in colliders) {
            if (c.gameObject.tag == "cat") {
                c.GetComponent<cat_movement>().Bomb(intensity);
            }
            else if (c.gameObject.tag == "box") {
                c.GetComponent<box_controller>().Bomb(intensity);
            }
        }
    }
}
