using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Prize : MonoBehaviour
{
    // Create variables
    public float borderX = 45;        //set border for movement on the X axis
    public float borderZ = 15;        //set border for movement on the Z axis

    // Start is called before the first frame update
    void Start() { // set this object to a random position at the start of the game
        transform.position = new Vector3(Random.Range(-borderX, borderX), 2, Random.Range(-borderZ, borderZ));
    }

    //Called when entering a collision
    void OnCollisionEnter(Collision other) {
         // Make the object appear at a random new position within the space
         transform.position = new Vector3 (
             Random.Range(-borderX, borderX), 2, Random.Range(-borderZ, borderZ));
             GameManager.instance.Score++;                       //change the score variable on the GameManager script
     }
}
