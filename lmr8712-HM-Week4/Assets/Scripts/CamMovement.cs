using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CamMovement : MonoBehaviour
{
    // Create variables
    public float sensitivity = 10f;                 //variable for mouse sensitivity
    public float smoothing = 5f;                    //variable to smooth movement
    public GameObject player;                       //variable to store a game object
    public float xRotation = 0f;                    //variable to manage the rotation on the x axis
    public float mouseX;                            //variable for horizontal movement
    public float mouseY;                            //variable for vertical movement
    
    // Update is called once per frame
    private void Start() {
        player = transform.parent.gameObject;       //store the transform of the parent object
        Cursor.lockState = CursorLockMode.Locked;   //lock the curse to center of the screen
    }
    
    // Update is called once per frame
    void Update() {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;                   //take input from mouse horizontal movement
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;                   //take input from mouse vertical movement
        xRotation -= mouseY;                                                                //invert standard camara
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);                         //establish limits for vertical movement
        transform.localRotation = UnityEngine.Quaternion.Euler(xRotation, 0f, 0f);      //rotate camara for vertical movement
        player.transform.Rotate(Vector3.up * mouseX);                                 //rotate parent object for horizontal movement
    }
}
