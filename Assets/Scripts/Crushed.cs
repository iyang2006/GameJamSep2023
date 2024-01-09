using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crushed : MonoBehaviour
{
    public Object dog;
    public Object cat;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == dog || collision.gameObject == cat)
        {
            UnityEngine.SceneManagement.Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
