using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_collider_script : MonoBehaviour
{

    private bool collidin;
    // Start is called before the first frame update
    void Start()
    {
        collidin = false;
    }

    void onTriggerEnter(Collision col) {
        if (col.gameObject.tag == "wall_tag") {
            collidin = true;
        }
    }

    void onTriggerExit(Collision col) {
        if (col.gameObject.tag == "wall_tag") {
            collidin = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isColliding() {
        return collidin;
    }
}
