using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_controller : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float stopTextTime;
    [SerializeField] private float fadeTime;
    [SerializeField] private Light sun;
    [SerializeField] private Rigidbody body;
    private bool fade;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        fade = false;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = body.position;
        vec.y = 1 * speed * Time.deltaTime;
        //body.MovePosition(body.position + vec);
        
        if (Time.time <= (stopTextTime + startTime)) {
            body.MovePosition(body.position + vec);
        }

        if ((Time.time >= (startTime + fadeTime + stopTextTime)) && !fade) {
            Debug.Log("turn off lgihts");
            sun.GetComponent<Light>().enabled = false;
            Destroy(body.gameObject);
            Application.Quit();
        }
    }
}
