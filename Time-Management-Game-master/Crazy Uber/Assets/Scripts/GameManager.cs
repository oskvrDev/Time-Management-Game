using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float timeForLevel;
    float timeLeft;

    [SerializeField]
    PauseMenu pauseMenu;
    [SerializeField]
    GameObject scoreMenu;
    [Header("Add the Score label here, from the scoring menu")]
    [SerializeField]
    //save the text box that will get the scoreboard text
    Text TextScoreBoard;
    [Header("Add the Score System script here")]
    [SerializeField]
    //save the Score system for us to be able to write how many points was made when the time ends
    ScoringSystem scoreSystem_;

    [SerializeField]
    bool race;

    [SerializeField]
    TimeSystem timeSystem;

    [SerializeField]
    RaceCheckpoints raceCheckpoints;


    void Start()
    {
        if(!race)
        {
            timeLeft = timeForLevel;
        }
        else
        {
            timeLeft = timeSystem.raceTime;
        }

        scoreMenu.SetActive(false);
    }

    private void Update() 
    {
        //decrease the time left to complete the level
        timeLeft -= Time.deltaTime;

        if(!race)
        {
            if (timeLeft <= 0)
            {
                //stop the game and show menu with scores
                Time.timeScale = 0f;
                pauseMenu.menu.SetActive(true);
                scoreMenu.SetActive(true);
                pauseMenu.UI.SetActive(false);
                pauseMenu.velocimeter.SetActive(false);
                pauseMenu.minimap.SetActive(false);
                pauseMenu.pauseMenu.SetActive(false);
                //this will write how many points the user made
                TextScoreBoard.text = "Score: " + scoreSystem_.totalScore;
            }
        }
        else
        {
            if (raceCheckpoints.raceFinished)
            {
                //stop the game and show menu with scores
                pauseMenu.menu.SetActive(true);
                scoreMenu.SetActive(true);
                pauseMenu.UI.SetActive(false);
                pauseMenu.velocimeter.SetActive(false);
                pauseMenu.minimap.SetActive(false);
                pauseMenu.pauseMenu.SetActive(false);
                //this will write how many points the user made
                TextScoreBoard.text = "Score: " + scoreSystem_.totalScore;
                Time.timeScale = 0f;
            }
        }
    }

    //IEnumerator ShowScoreMenu()
    //{
    //    yield return new WaitForSeconds(3f);

    //    //stop the game and show menu with scores
    //    Time.timeScale = 0f;
    //    pauseMenu.menu.SetActive(true);
    //    scoreMenu.SetActive(true);
    //    pauseMenu.UI.SetActive(false);
    //    pauseMenu.velocimeter.SetActive(false);
    //    pauseMenu.minimap.SetActive(false);
    //}
}
