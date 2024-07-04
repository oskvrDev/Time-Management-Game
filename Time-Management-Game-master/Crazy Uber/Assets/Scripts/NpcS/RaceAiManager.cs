using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UI;

public class RaceAiManager : MonoBehaviour
{
    [SerializeField]
    //this will store all the ai cars
    RaceCarScript[] aiRaceCars;

    [SerializeField]
    //this will save the player car
    RaceCheckpoints raceChecpointScript;

    [SerializeField]
    Text textToDisplayPos;

    [SerializeField]
    float maxspeed_;
    [SerializeField]
    //this is the speed that will be increased per second
    float speedToIncreasePerSecond_;

    //this will get the current position (from 1 to 4) this is only to be able to send the information to the Scoreboard
    public int currentPos;

    // Start is called before the first frame update
    void Start()
    {
        //this foreach will assign the speed to every ai car
        //foreach (RaceCarScript n in aiRaceCars)
        //{
        //    n.maxSpeed = maxspeed_;
        //    n.speedToIncreasePerSecond = speedToIncreasePerSecond_;
        //}


        //create an array from 1 to 3 (number of cars racing)
        int[] arr = { 0, 1, 2 };
        //inside the brackets im going to randomize the array above, so that i can then change the speed randomly
        {
            //for this i used this link:https://www.delftstack.com/howto/csharp/shuffle-array-in-csharp/
            System.Random random = new System.Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();
        }

        //make this variable the same as 90 percent of the speed to increase, this will be used to randomly decrease speed to the ai cars
        float speedToDecrease = speedToIncreasePerSecond_ * 0.9f;
        //do a foreach to decrease the ai cars speed
        foreach (int n in arr)
        {
            //make the current speed to increase times 0.9, 1 or 1,1
            aiRaceCars[n].speedToIncreasePerSecond = aiRaceCars[n].speedToIncreasePerSecond * speedToDecrease;
            speedToDecrease = speedToDecrease + 0.1f;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();
    }

    //this function will check at wich pos the player is
    private void CheckPlayerPosition()
    {
        //this will be 4 if hes last, 1 if hes first
        int decreasePosition = 1;
        //this foreach will check every ai car and see if they are in front of him, if they are, we increase the decrease position
        foreach(RaceCarScript n in aiRaceCars)
        {
            //if the ainpc car is on the same checkpoint as the player
            if(n.currentPath == raceChecpointScript.currentCheckpoint )
            {
                //check if the ai car is closer to the checkpoint then the player, if he is decrease a position
                if (n.ReturnDistanceFromCheckPoint() < raceChecpointScript.ReturnPlayerDistanceFromCheckPoint() || n.finishedRace==true)
                {
                    decreasePosition += 1;
                }
            }
            else if (n.currentPath > raceChecpointScript.currentCheckpoint)
            {
                //if the ai is in a bigger checkpoint decrease the position
                
                decreasePosition += 1;   
                
            }
            else if (n.finishedRace == true)
            {
                decreasePosition += 1;
            }
        }
        currentPos = decreasePosition;
        textToDisplayPos.text = "Player Position: " + decreasePosition + "/4";

    }
}
