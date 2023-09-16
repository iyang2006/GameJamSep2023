using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow)) {
            movement.x = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            movement.x = -1;
        }
        else {
            movement.x = 1;
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.fixedDeltaTime));
    }
}
