using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject targetPlatform;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Detected: " + collider.gameObject.layer + " | " + collider.gameObject.tag);
        if (collider.gameObject.layer == 10)
        {
            Debug.Log("Button Entered");
            Platform platform = targetPlatform.GetComponent<Platform>();
            platform.Activate();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 10)
        {
            Debug.Log("Button Exited");
            Platform platform = targetPlatform.GetComponent<Platform>();
            platform.Deactivate();
        }
    }


}
