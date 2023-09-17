using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject targetPlatform;
    [SerializeField] private AudioSource activationSound;
    [SerializeField] private AudioSource deactivationSound;
    private ArrayList colliders;

    private Color activatedColor = new Color(45f/255f, 241f/255f, 163f/255f, 1f);
    private Color deactivatedColor = new Color(255f/255f, 160f/255f, 61f/255f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        colliders = new ArrayList();
        GetComponent<SpriteRenderer>().color = deactivatedColor;
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
                GetComponent<SpriteRenderer>().color = activatedColor;
                activationSound.Play();
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
                GetComponent<SpriteRenderer>().color = deactivatedColor;
                deactivationSound.Play();
            }
            if (colliders.Contains(collider))
            {
                colliders.Remove(collider);
            }
        }
    }


}
