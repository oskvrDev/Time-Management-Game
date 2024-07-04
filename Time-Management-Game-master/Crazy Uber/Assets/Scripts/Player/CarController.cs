using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{   
    //this will get the audio of the car engine, and will control its pitch
    public AudioSource carEngineSound;

    [SerializeField]
    //this will get the scoring system component
    ScoringSystem scoreSystem;

    [SerializeField]
    //this will get the random, location component
    RandomLocation randomloc;

    //this will get the rigidbody component
    Rigidbody2D rb;


    /*DO NOT CHANGE THIS VARIABLES, BECAUSE THE SOUND AND AND THE VELOCIMETER, AND THE STEERING DEPENDS ON IT*/

    //this will get the speed
    const float accelerationPower = 20f;
    
    //this will be the max speed
    const float maxSpeed = 101.23f;

    //this will get the steering power
    private float steeringPower = 0.6f;

    
    //steering amount will get arrowv axis values, speed is equal to the player arrow axis times aceleration, and directions will be used for knowing if is drifing forward or backwards, it will return 1 or -1
    float steeringAmount, speed, direction, speedSmoother;

    //this will get the pickup locations from the random locations script
    Transform pickupLocation, returnLocation;
    [HideInInspector]
    public bool hasPickedUpPerson = false;
    bool returnLocationEnabled;
    

    // Use this for initialization
    void Start()
    {
        //this is a temporary variable that will be used to get the audio Gameobject
        GameObject AudioManager;
        //this will get the audio object
        AudioManager = GameObject.FindGameObjectWithTag("Audio");
        //this will save the audio source of the car engine, in the variable carenginesound
        carEngineSound = AudioManager.GetComponent<AudioSource>();
        returnLocationEnabled = false;
        hasPickedUpPerson = false;
        //this will get this car rigid body
        rb = GetComponent<Rigidbody2D>();
        //generate a pickup and drop of location
        GetPickupLocation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CarMove();
       
    }

    private void Update()
    {
        if(hasPickedUpPerson && !returnLocationEnabled)
        {
            //enable the return location after the player has picked up person from pick up location
            EnableReturnLocation();
        }
    }

    void CarMove()
    {
        //get 1 to -1 values
        steeringAmount = -Input.GetAxis("Horizontal");
        //this switch will controll, if the player is acelerating, or maintaining speed, or braking, or going backwards
        switch (Input.GetAxis("Vertical"))
        {
            //if the axis is 1 (the player is pressing top arrow), increase speed
            case float n when n >= 1:
                //if the player hasnt reached the top speed, increase the speed smoother (variable, that will increase by time
                if (speed < maxSpeed)
                {
                    //increase 0.3 the speedsmoother per second
                    speedSmoother = speedSmoother + 0.3f * Time.deltaTime;
                }
                //make speed the value of the axis (1 to -1) , times the acceleration multiplicaded by the speed smoother
                speed = Input.GetAxis("Vertical") * (accelerationPower * speedSmoother);
                break;
            //if the axis is less then 0 and the speed bigger then 0 (the player is pressing bottom arrow while the car is going forward, so he is breacking), decrease speed
            case float n when n < 0 && speed >0 && speedSmoother > 0:
                
                //this if will fix the problem of the car taking to long to reverse when the car had a lot of acumulated speed
                if (rb.velocity== new Vector2(0,0))
                {
                    speed = 0;
                    speedSmoother = 0;
                }
                //decrease speed smoother, because the player is bracking
                speedSmoother = speedSmoother - 1.4f * Time.deltaTime;
                //if the speedsmoother is less then 0, put it to zero, so that it doesnt break to the point of being negative
                if (speedSmoother < 0)
                    speedSmoother = 0;
                //decrease the speed, i dont use the axis, or the speed would be negative, because the axis is less then 0
                speed = accelerationPower * speedSmoother;
                break;
            //if the user is clicking bottom arrow and the speed is less then 0, the user is going backwards
            case float n when n <= -1 && speed<=0:
                //if the user isnt top reverse speed (wich is max speed divide by 4) i do *-1 so that the max speed is negative
                if(speed>(maxSpeed /4) * -1)
                {
                    //increase the speedsmoother by 0.5 per second
                    speedSmoother = speedSmoother + 0.5f * Time.deltaTime;
                    //do -1 times accelarationPower times SpeedSmother (will give a negative number, and move backwards)
                    speed = Input.GetAxis("Vertical") * (accelerationPower * speedSmoother);
                }                
                break;
            //if the player speed is less then 0, and user axis is bigger then -1 the user is desacelerating from reverse
            case float n when n >= -1 && speed < 0:
                //make speed smoother negative
                speedSmoother = speedSmoother * -1;
                //and do speed smoother + 2, times seconds ( will give a negative number)
                speedSmoother = speedSmoother + 1f * Time.deltaTime;
                //if the speed smoother is bigger then 0 make it zero, so that he doesnt acelerate whilde desacelerates from reverse
                if (speedSmoother > 0)
                    speedSmoother = 0;
                //decesalerate
                speed = accelerationPower * speedSmoother;
                // speed = speed * -1;

                break;
        }

        //this function will edit the steering and the audio using the player speed as the reference to edit it.
        EditSteeringAndAudio();


        //will get 1 if is positive, and -1 if is negative
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        //rotate the car
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;
        //move the car forward
        rb.AddRelativeForce(Vector2.up * speed);
        //move the car to the right and left
        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
    }

    void EditSteeringAndAudio()
    {
        //this 2 counts will give a result of steering power from 0.8 to 0.19, the faster the car goes, the less it turns, so at max speed the steering will be 0.19
        steeringPower = (speed * 0.6f) / 101;
        steeringPower = 0.8f - steeringPower;        
        
        //this will change the pitch from 1 to 4, depending on the rigid body speed,(goes from 0-20)
        carEngineSound.pitch = ((rb.velocity.magnitude / 20) * 3) + 1;
        
    }

    public void GetPickupLocation()
    {
        //generate pickup and return locatrions
        randomloc.ChooseRandomLocations();

        //Get pickup and return locations
        pickupLocation = randomloc.ReturnPickUpLocation();
        //returnLocation = randomloc.ReturnDropOffLocation();

        //Enable the images for thje return and pickup and change its color,  and activate the box collider
        pickupLocation.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //returnLocation.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        pickupLocation.gameObject.GetComponent<SpriteRenderer>().color= Color.green;
        //returnLocation.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        pickupLocation.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //returnLocation.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        returnLocationEnabled = false;

    }

    void EnableReturnLocation()
    {
        //Enable the images for the return location and change its color,  and activate the box collider
        returnLocation = randomloc.ReturnDropOffLocation();
        returnLocation.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        returnLocation.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        returnLocation.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        returnLocationEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the car runned him over
        if (collision.CompareTag("Npc"))
        {
            scoreSystem.RemoveStar();   
        }

        if(collision.CompareTag("NpcCar"))
        {
            scoreSystem.RemoveStar();
        }
    }

 

}
