using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject targetPlatform;
    [SerializeField] private bool activeRight = true;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        if (activeRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
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
            bool leftWall = false;
            if (collider.gameObject.layer == 9)
            {
                leftWall = collider.gameObject.GetComponent<cat_movement>().leftWall;
            }
            else if (collider.gameObject.layer == 10)
            {
                leftWall = collider.gameObject.GetComponent<dog_movement>().leftWall;
            }


            Platform platform = targetPlatform.GetComponent<Platform>();
            if (!leftWall)
            {
                if (activeRight && !active)
                {
                    active = true;
                    platform.Activate();
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                } else if (!activeRight && active)
                {
                    active = false;
                    platform.Deactivate();
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
            } else if (leftWall)
            {
                if (!activeRight && !active)
                {
                    active = true;
                    platform.Activate();
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                } else if (activeRight && active)
                {
                    active = false;
                    platform.Deactivate();
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
            }

        }
    }

}
