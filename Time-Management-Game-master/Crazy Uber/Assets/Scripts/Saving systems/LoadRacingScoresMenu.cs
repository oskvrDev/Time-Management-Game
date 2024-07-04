using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRacingScoresMenu : MonoBehaviour
{
    class RaceScoreBoard
    {
        // Constructor that takes points and number of records, this constructor will be called if the user doesnt insert the name, and will assing the name to unknown
        public RaceScoreBoard(int points, int numberOfRecords)
        {

            playerName = "unknown";
            playerPoints = points;
            numberOfSaves = numberOfRecords + 1;

        }

        // Constructor that takes all the arguments, this constructor will set the name, points and number of records
        public RaceScoreBoard(string name, int points, int numberOfRecords)
        {
            playerName = name;
            playerPoints = points;
            numberOfSaves = numberOfRecords + 1;
        }

        //Create a read and set variable called player name that will save the player name
        public string playerName { get; set; }

        //Create a read and set variable called Player points that will save the player points
        public int playerPoints { get; set; }

        //Create a read only variable called number of saves
        public int numberOfSaves { get; }
    }

    //this will save all the player scores
    List<RaceScoreBoard> allPlayerScores = new List<RaceScoreBoard>();
    [Header("Put five labels here that are going to display player points")]
    [SerializeField]
    //this will store every textbox that will display all the player points
    Text[] allPointsDisplayer;
    [Header("Put the name of the scene, for example Race 1")]
    //we need to say the race name for the saving system to know wich race it is, and to create diferent save files.
    public string raceName;
    public bool isAiRace = false;
    //this function will load all the points stored
    public void LoadPlayerScores()
    {
        //clear the list
        allPlayerScores.Clear();

        //do a loop trought all the scores to get all of the scores
        for (int i = 0; i < PlayerPrefs.GetInt(raceName + "NumberOfItems") + 2; i++)
        {
            // Debug.Log(PlayerPrefs.GetString("Name" + i));
            //Debug.Log(PlayerPrefs.GetInt("Points" + i));
            //add all the enemies to the allplayer list
            allPlayerScores.Add(new RaceScoreBoard(PlayerPrefs.GetString(raceName + "Name" + i), PlayerPrefs.GetInt(raceName + "Points" + i), PlayerPrefs.GetInt(raceName + "NumberOfItems")));
        }

        //do a sorting loop
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            //this loop will check every item with the items from the other for
            for (int x = 0; x < allPlayerScores.Count - 1; x++)
            {
                //check if the item is bigger then this item, if it is just move it up
                if (allPlayerScores[i].playerPoints > allPlayerScores[x].playerPoints)
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
            if (i > 4)
            {
                allPlayerScores.RemoveAt(i);
            }
            else
            {
                //this will display the points to the user
                allPointsDisplayer[i].text = "Name: " + allPlayerScores[i].playerName + " Race Points: " + allPlayerScores[i].playerPoints;
                //output
                Debug.Log("name " + allPlayerScores[i].playerName + " race points " + allPlayerScores[i].playerPoints);
            }

        }
    }

    public void LoadPlayerScoresAiRace()
    {
        //clear the list
        allPlayerScores.Clear();

        //do a loop trought all the scores to get all of the scores
        for (int i = 0; i < PlayerPrefs.GetInt(raceName + "NumberOfItems") + 2; i++)
        {
            // Debug.Log(PlayerPrefs.GetString("Name" + i));
            //Debug.Log(PlayerPrefs.GetInt("Points" + i));
            //add all the enemies to the allplayer list
            allPlayerScores.Add(new RaceScoreBoard(PlayerPrefs.GetString(raceName + "Name" + i), PlayerPrefs.GetInt(raceName + "Points" + i), PlayerPrefs.GetInt(raceName + "NumberOfItems")));
        }

        //do a sorting loop
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            //this loop will check every item with the items from the other for
            for (int x = 0; x < allPlayerScores.Count - 1; x++)
            {
                //check if the item is bigger then this item, if it is just move it up
                if (allPlayerScores[i].playerPoints < allPlayerScores[x].playerPoints)
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

        //remove the 0 value from the list
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            if(allPlayerScores[i].playerPoints== 0)
            {
                allPlayerScores.RemoveAt(i);
            }
        }

        //do a loop for output
        for (int i = 0; i < allPlayerScores.Count - 1; i++)
        {
            //this will make so that we dont have more then 9 items
            if (i > 4)
            {
                allPlayerScores.RemoveAt(i);
            }
            else
            {
                //this will display the points to the user
                allPointsDisplayer[i].text = "Name: " + allPlayerScores[i].playerName + " Race Position: " + allPlayerScores[i].playerPoints;
                //output
                Debug.Log("name " + allPlayerScores[i].playerName + " race points " + allPlayerScores[i].playerPoints);
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isAiRace == false)
            LoadPlayerScores();
        else
            LoadPlayerScoresAiRace();
    }
}
