using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject targetPlatform;
    private ArrayList colliders;


    // Start is called before the first frame update
    void Start()
    {
        colliders = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Detected: " + collider.gameObject.layer + " | " + collider.gameObject.tag);
        if (collider.gameObject.layer == 10 || collider.gameObject.layer == 12)
        {
            Debug.Log("Button Entered | Count: " + colliders.Count);
            Platform platform = targetPlatform.GetComponent<Platform>();

            if (colliders.Count == 0)
            {
                platform.Activate();
            }
            colliders.Add(collider);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 10 || collider.gameObject.layer == 12)
        {
            Debug.Log("Button Exited");
            Platform platform = targetPlatform.GetComponent<Platform>();

            if (colliders.Count == 1)
            {
                platform.Deactivate();
            }
            if (colliders.Contains(collider))
            {
                colliders.Remove(collider);
            }
        }
    }


}
