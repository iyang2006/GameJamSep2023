using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class camera_controller : MonoBehaviour
{

    [SerializeField] private float shakeIntensity;
    [SerializeField] public GameObject dog;
    [SerializeField] public GameObject cat;
    [SerializeField] private Transform trans;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;
    [SerializeField] private float maxZ;
    [SerializeField] private float maxOsc;
    private float iscalar;
    // Start is called before the first frame update
    private Transform dogT;
    private Transform catT;
    private float shakeInit;
    [SerializeField] private float shakeDecrease;
    [SerializeField] private float shakeSpeed;
    void Start()
    {
        dogT = dog.GetComponent<Transform>();
        catT = cat.GetComponent<Transform>();
        Vector3 camPos = trans.position;
        camPos.z = maxZ;
        trans.position = camPos;
        shakeInit = (-maxOsc) - 1;
        iscalar = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dogPos = dogT.position;
        Vector3 catPos = catT.position;
        Vector3 camPos = trans.position;

        float avgX = (dogPos.x + catPos.x) / 2;
        float avgY = (dogPos.y + catPos.y) / 2;

        float dist = (float)Math.Sqrt(Math.Pow((float)(dogPos.x - catPos.x), 2f) + Math.Pow((float)(dogPos.y - catPos.y), 2f));

        camPos.x = avgX + xOffset;
        camPos.y = avgY + yOffset;

        if ((-1 * (dist + zOffset)) >= maxZ) {
            camPos.z = maxZ;
        }
        else {
            camPos.z = -1 * (dist + zOffset);
        }
        
        float dt = Time.time - shakeInit;
        if (dt <= maxOsc) {
            float shakeY = (float) (Math.Exp(-shakeDecrease * (1/iscalar) * dt) * Math.Cos(shakeSpeed * dt * Math.PI));
            camPos.y += shakeY * shakeIntensity * iscalar;
        }

        trans.position = camPos;
    }

    public void shakeScreen(float inten) {
        Debug.Log("pound intensity: " + inten);
        iscalar = inten;
        shakeInit = Time.time;
    }
}
