using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Create variables
    public static GameManager instance;                     //static variable that will hold Singleton
    public Text text;                                       //place to store text
    private int score = 0;                                  //variable to keep score
    public GameObject image;

    // Allows other places to access and modify the score variable
    public int Score {
        get { return score; }
        set { score = value;
        }
    }

    //variable and object that control the state of the game
    public float lives = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private bool isGame = true;
    
    // Create List
    private List<int> highScores;

    // Create Constance
    private const string FILE_HIGH_SCORES = "/highScores.txt";
    private string FILE_PATH_HIGH_SCORE;

    // Called when script instance is being loaded
    void Awake()
    {
        // Make a Singleton to prevent more than one instance of an object
        if (instance == null) {
            //if instance hasn't been set
            DontDestroyOnLoad(gameObject);              //don't destroy object when loading new scene
            instance = this;                            //set instance to this object 
        }
        else {
            //if instance is set to an object
            Destroy(gameObject);                        //destroy this object
        }
    }

    // Start is called before the first frame update
    void Start() {
        lives = 3;
        FILE_PATH_HIGH_SCORE = Application.dataPath + FILE_HIGH_SCORES; //Find the path where the High Score data is going to be stored
    }
    
    // Update is called once per frame
    void Update() {
        //if not in the game, display high scores board
        if (!isGame) {
            string highScoreString = "High Scores\n";
            
            //update high score board
            for (var i = 0; i < highScores.Count; i++) {
                highScoreString += highScores[i] + "\n";
            }
            text.text = highScoreString;
        }
        else {// While in the game, display the score
            text.text = "Score: " + score + "";
        }
        
        if (lives == 2) {
            Destroy(heart3);
        }

        if (lives == 1) {
            Destroy(heart2);
        }
        
        // Got to GameOver Scene, clear canvas, and update the High Score
        if (lives == 0 && isGame) {
            Destroy(heart1);
            Destroy(image);
            SceneManager.LoadScene(1);
            isGame = false;
            UpdateHighScores();
        }
    }
    
    
    void UpdateHighScores() {
        if (highScores == null) {
            highScores = new List<int>();
            
            string fileContents = File.ReadAllText(FILE_PATH_HIGH_SCORE);
            string[] fileScores = fileContents.Split(',');

            for (var i = 0; i < fileScores.Length - 1; i++) {
                highScores.Add(Int32.Parse(fileScores[i]));
            }
        }

        // Go over the list of high score and stop once the score is inserted to the table
        for (var i = 0; i < highScores.Count; i++) {
            if (score > highScores[i]) {
                highScores.Insert(i, score);
                break;
            }
        }

        string saveHighScoreString = "";
        
        for (var i = 0; i < highScores.Count; i++) {
            saveHighScoreString += highScores[i] + ",";
        }
        File.WriteAllText(FILE_PATH_HIGH_SCORE, saveHighScoreString);
    }
}