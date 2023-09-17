using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject targetPlatform;
    [SerializeField] private bool activeRight = true;
    private bool active;
    //private bool right;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        //right = !activeRight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Detected: " + collider.gameObject.layer + " | " + collider.gameObject.tag);
        if (collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
        {
            //Debug.Log("Lever Entered: " + collider.gameObject.GetComponent<Rigidbody>().velocity.x);
            //GameObject player = collider.gameObject;
            Rigidbody body = collider.gameObject.GetComponent<Rigidbody>();
            //Vector3 velocity = body.velocity;
            //Debug.Log("Lever Entered: " + velocity.x);

            Platform platform = targetPlatform.GetComponent<Platform>();
            if (body.velocity.x > 0)
            {
                if (activeRight && !active)
                {
                    active = true;
                    platform.Activate();
                } else if (!activeRight && active)
                {
                    active = false;
                    platform.Deactivate();
                }
            } else if (body.velocity.x < 0)
            {
                if (!activeRight && !active)
                {
                    active = true;
                    platform.Activate();
                } else if (activeRight && active)
                {
                    active = false;
                    platform.Deactivate();
                }
            }


            //Platform platform = targetPlatform.GetComponent<Platform>();
            //platform.Activate();
        }
    }

    //private void OnTriggerExit(Collider collider)
    //{
        //if (collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
        //{
            //Debug.Log("Lever Exited");
            //Platform platform = targetPlatform.GetComponent<Platform>();
            //platform.Deactivate();
        //}
    //}
}
