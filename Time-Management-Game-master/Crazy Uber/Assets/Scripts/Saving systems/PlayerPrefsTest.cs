using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsTest : MonoBehaviour
{
    class ScoreBoard
    {
        // Constructor that takes points and number of records, this constructor will be called if the user doesnt insert the name, and will assing the name to unknown
        public ScoreBoard(int points, int numberOfRecords)
        {
            
            playerName = "unknown";
            playerPoints = points;
            numberOfSaves = numberOfRecords + 1;
            
        }

        // Constructor that takes all the arguments, this constructor will set the name, points and number of records
        public ScoreBoard(string name, int points, int numberOfRecords)
        {
            playerName = name;
            playerPoints = points;
            numberOfSaves = numberOfRecords +1;
        }

        //Create a read and set variable called player name that will save the player name
        public string playerName { get; set; }

        //Create a read and set variable called Player points that will save the player points
        public int playerPoints { get; set; }

        //Create a read only variable called number of saves
        public int numberOfSaves { get; }        
    }


    //create this player score to store the current points and name
    ScoreBoard playerScore;
    //this will save all the player scores
    List<ScoreBoard> allPlayerScores = new List<ScoreBoard>();
    [Header("Put five labels here that are going to display player points")]
    [SerializeField]
    //this will store every textbox that will display all the player points
    Text[] allPointsDisplayer;
    
    [SerializeField]
    //save the text box that will get the name of the player
    Text TextNameOfPlayer;
    [SerializeField]
    PauseMenu pauseMenu_;
    [SerializeField]
    //this will save the score to add
    ScoringSystem totalScore;

    //this function is going to call the set player score
    public void CallerOfSetPlayerScore()
    {
        //call the function to create this score
        SetPlayerScore(totalScore.totalScore, TextNameOfPlayer.text);
    }

    //this function add the points, name, to the player, and then it calls the saving system
    private void SetPlayerScore(int playerPoints_, string playerName_ = "unknown")
    {
        //give the currect values of this player score
        playerScore = new ScoreBoard(playerName_, playerPoints_, PlayerPrefs.GetInt("NumberOfItems"));
        SavePlayerScore();
    }

    //this function will load all the points stored
    public void LoadPlayerScores()
    {
        //clear the list
        allPlayerScores.Clear();

        //do a loop trought all the scores to get all of the scores
        for (int i = 0; i < PlayerPrefs.GetInt("NumberOfItems") +2; i++)
        {
           // Debug.Log(PlayerPrefs.GetString("Name" + i));
            //Debug.Log(PlayerPrefs.GetInt("Points" + i));
            //add all the enemies to the allplayer list
            allPlayerScores.Add(new ScoreBoard(PlayerPrefs.GetString("Name" + i), PlayerPrefs.GetInt("Points" + i), PlayerPrefs.GetInt("NumberOfItems")));
        }
        
        //do a sorting loop
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            //this loop will check every item with the items from the other for
            for (int x = 0; x < allPlayerScores.Count - 1; x++)
            {
                //check if the item is bigger then this item, if it is just move it up
                if(allPlayerScores[i].playerPoints>allPlayerScores[x].playerPoints)
                {
                    int temp = allPlayerScores[x].playerPoints;
                    string tempName = allPlayerScores[x].playerName;
                    allPlayerScores[x].playerPoints = allPlayerScores[i].playerPoints;
                    allPlayerScores[i].playerPoints = temp;
                    allPlayerScores[x].playerName = allPlayerScores[i].playerName;
                    allPlayerScores[i].playerName = tempName;
                }
            }
            
        }
        //do a loop for output
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            //this will make so that we dont have more then 9 items
            if(i > 4)
            {
                allPlayerScores.RemoveAt(i);
            }
            else
            {
                //this will display the points to the user
                allPointsDisplayer[i].text = "Name: " + allPlayerScores[i].playerName + " Points: " + allPlayerScores[i].playerPoints;
                //output
                Debug.Log("name " + allPlayerScores[i].playerName + " points " + allPlayerScores[i].playerPoints); 
            }

        }
    }

    //this function will save all the points
    private void SavePlayerScore()
    {
        //LoadPlayerScores();
        
        //this will set all the variables with nameN (the n is the number of all the saves +1) so it will create a new player pref because it has a number at the end making is name different from the older prefs
        PlayerPrefs.SetString("Name" + playerScore.numberOfSaves, playerScore.playerName);
        PlayerPrefs.SetInt("Points" + playerScore.numberOfSaves, playerScore.playerPoints);
        PlayerPrefs.SetInt("NumberOfItems", playerScore.numberOfSaves);
        PlayerPrefs.Save();

        pauseMenu_.LoadMainMenu();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerScores();
        //PlayerPrefs.DeleteAll();
    }



  
}
