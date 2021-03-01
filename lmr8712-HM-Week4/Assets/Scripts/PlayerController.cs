using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    
    //create variables
    public float moveForce;                     //variable for movement speed
    public float jumpForce;                     //variable for jump
    public float raycastDistance;               //variable for when determining if object is on the floor 
    private Rigidbody rb;                       //variable for the Rigidbody
    public static PlayerController instance;    //static variable that will hold Singleton

    //Called when script instance is being loaded
    void Awake() {   
        //make a Singleton to prevent more than one instance of an object
        if (instance == null) {                 //if instance hasn't been set
            DontDestroyOnLoad(gameObject);      //don't destroy object when loading new scene
            instance = this;                    //set instance to this object 
        }
        
        else {                                  //if instance is set to an object
            Destroy(gameObject);                //destroy this object
        }
    }
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();         //get the Rigidbody of the object
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //make movement relative to direction of the camera by getting the raw Axis form X and Z
        Vector3 newPosition = rb.position + 
                              rb.transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0,
                                  Input.GetAxisRaw("Vertical"));
        rb.MovePosition(newPosition);
        
        //player's movement
        if (Input.GetKey(KeyCode.W)) {                          //when pressing W
            rb.AddForce(Vector3.forward * moveForce);           //add fore to rb to move forward
        }

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(Vector3.left * moveForce);              //add fore to rb to move left
        }

        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector3.back * moveForce);               //add fore to rb to move back
        }

        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right * moveForce);             //add fore to rb to move right
        }
    }
    
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {                              //if Space bar is press
            if (Grounded()) {                                               //if object on the ground
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);     //add force to rb to move up
            }
        }
    }

    //Check if object is on ground
    bool Grounded() {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }

    //Called when entering a collision
    private void OnCollisionEnter(Collision other) {
        if (other.transform.gameObject.tag.Equals("Respawn") == true) { //when colliding with an object with the Respawn 
            this.transform.position = new Vector3(0, 2, 0);       //move object to the center of the space
            GameManager.instance.lives--;                               //change the lives variable on the GameManager script

            // Destroy object when losing last live
            if (GameManager.instance.lives == 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
