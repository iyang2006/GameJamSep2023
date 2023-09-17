using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBox : MonoBehaviour
{
    [SerializeField] private String nextSceneName;
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
        if (collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
        {
            Debug.Log("ExitBox Entered | Count: " + colliders.Count);

            if (colliders.Count == 1)
            {
                if (SceneManager.GetSceneByName(nextSceneName) != null)
                {
                    SceneManager.LoadScene(nextSceneName);
                } else
                {
                    Debug.Log("ExitBox: Next Scene not Found!!!!");
                }
            }
            colliders.Add(collider);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
        {
            Debug.Log("ExitBox Exited");
            if (colliders.Contains(collider))
            {
                colliders.Remove(collider);
            }
        }
    }


}
