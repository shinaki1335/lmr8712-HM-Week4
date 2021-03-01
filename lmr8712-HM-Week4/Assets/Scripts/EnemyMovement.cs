using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    // Create variables
    public int border = 14;             //set border of the space
    public int speed;               //set speed movement
    private Rigidbody rb;               //variable for the Rigidbody
    
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>(); //get the Rigidbody of the object
        speed = Random.Range(-5, 5);    //set a random speed for the objet
    }

    // Update is called once per frame
    void Update() {   
        // Move enemy
        Vector3 newPosition = transform.position;       //get current position of GameObject
        newPosition.z += speed;                         //move newPosition forward
        rb.MovePosition(newPosition);                   //set enemy's position to newPosition

        if (speed == 0) {                               //prevent object from having zero speed
            speed = Random.Range(-5, 5);                //set a random speed for the objet
        }
    }

    //Called when entering a collision
    private void OnCollisionEnter(Collision other) {   
        //make object change direction when reaching an specific position
        if (transform.position.z > border || transform.position.z < -border) {
            speed *= -1;
        }
    }
}