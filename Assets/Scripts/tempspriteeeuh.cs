using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempspriteeeuh : MonoBehaviour
{
    [SerializeField] float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        GetComponent<SpriteRenderer>().size = new Vector2(scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
