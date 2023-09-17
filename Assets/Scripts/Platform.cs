using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Platform : MonoBehaviour
{

    [SerializeField] private Vector3 activePosition = Vector3.zero;
    private Vector3 unactivePosition;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Rigidbody body;
    private bool active;
    private Vector3 velocity;
    private float XpositionDistance;
    private float YpositionDistance;


    // Start is called before the first frame update
    void Start()
    {
        active = false;
        unactivePosition = body.position;
        velocity = body.velocity;
        XpositionDistance = Mathf.Abs(unactivePosition.x - activePosition.x);
        YpositionDistance = Mathf.Abs(unactivePosition.y - activePosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && body.position != activePosition)
        {
            velocity.x = (activePosition.x - unactivePosition.x) * moveSpeed;
            velocity.y = (activePosition.y - unactivePosition.y) * moveSpeed;
        }

        else if (!active && body.position != unactivePosition)
        {
            velocity.x = (unactivePosition.x - activePosition.x) * moveSpeed;
            velocity.y = (unactivePosition.y - activePosition.y) * moveSpeed;
        }

        else
        {
            velocity = Vector3.zero;
        }

        body.velocity = velocity;
        PlatformBound();
    }

    public void Activate()
    {
        Debug.Log("Platform Activated");
        active = true;
    }

    public void Deactivate()
    {
        Debug.Log("Platform Deactivated");
        active = false;
    }

    private void PlatformBound()
    {

        if (active)
        {
            if (XpositionDistance < Mathf.Abs(body.position.x - unactivePosition.x) ||
            YpositionDistance < Mathf.Abs(body.position.y - unactivePosition.y))
            {
                Debug.Log("Platform Stopping");
                velocity = Vector3.zero;
                body.velocity = velocity;
                body.position = activePosition;
                Debug.Log("Platform Stopped");
            }
        } else
        {
            if (XpositionDistance < Mathf.Abs(body.position.x - activePosition.x) ||
            YpositionDistance < Mathf.Abs(body.position.y - activePosition.y))
            {
                Debug.Log("Platform Stopping");
                velocity = Vector3.zero;
                body.velocity = velocity;
                body.position = unactivePosition;
                Debug.Log("Platform Stopped");
            }
        }
    }


}
