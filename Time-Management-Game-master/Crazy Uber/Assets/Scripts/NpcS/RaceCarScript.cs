using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCarScript : MonoBehaviour
{
    [Header("For this script to work i need race ai manager script on game manager")]
    //this will  get the car sprite controller
    [SerializeField]
    CarSpritesController spriteController;


    [SerializeField]
    //this will give access to the path script
    Paths paths;
   
    //this will get all paths to follow
    [HideInInspector]
    public Transform[] pathsToFollow;

    //this will get the npc speed 
    public float maxSpeed;
    [HideInInspector]
    private float currentSpeed;
    public float speedToIncreasePerSecond;

    //this will get the current path
    [HideInInspector]
    public int currentPath;

    //this is true if the npc needs to stop
    [HideInInspector]
    public bool finishedRace = false;


    //this will get the current rigidbody
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        pathsToFollow = paths.paths;
        //this will assign a random car image to the gameobject
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteController.ReturnRandomCar();
    }

    // Update is called once per frame
    void Update()
    {
        //if the race hasnt finished yet for this car
        if(finishedRace == false)
        { 
            MoveRaceCar();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finishedRace == false)
        {
            //if we are not at max speed yet increase the speed
            if(currentSpeed < maxSpeed)
                currentSpeed = currentSpeed + speedToIncreasePerSecond * Time.deltaTime;

            //move the npc to the current path
            transform.position = Vector2.MoveTowards(transform.position, pathsToFollow[currentPath].position, Time.deltaTime * currentSpeed);
            transform.up = pathsToFollow[currentPath].position - transform.position;
        }
    }

    //this will return the current distance between this car and the checkpoint, this will be accessed by the RaceAiManager
    public float ReturnDistanceFromCheckPoint() { return Vector2.Distance(this.transform.position, pathsToFollow[currentPath].position); }

    private void MoveRaceCar()
    {
        //if the npc is on the same pos that the path
        if (Vector2.Distance(this.transform.position, pathsToFollow[currentPath].position) < 1)
        {
            //if we are at the last position pause the car
            if (currentPath + 1 >= pathsToFollow.Length)
            {
                finishedRace = true;
            }
            else
            { 
                //go to the next position if we are not at the last position
                currentPath = currentPath + 1;
            }
        }
    }
}
