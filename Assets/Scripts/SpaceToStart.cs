using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToStart : MonoBehaviour
{

    [SerializeField] private string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (SceneManager.GetSceneByName(nextSceneName) != null)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("ExitBox: Next Scene not Found!!!!");
            }
        }
    }
}
