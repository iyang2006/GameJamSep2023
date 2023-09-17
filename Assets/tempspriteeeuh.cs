using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempspriteeeuh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        GetComponent<SpriteRenderer>().size = new Vector2(2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
