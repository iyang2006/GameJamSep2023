using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_controller : MonoBehaviour
{

    [SerializeField] private Rigidbody body;
    [SerializeField] private float poundIntensity;
    private bool grounded;

    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            Debug.Log("box grounded");
            grounded = true;
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            grounded = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
