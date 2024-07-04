using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    //this will get the aicar initial pos
    Vector2 initialPos;
    //this will  get the car sprite controller
    [SerializeField]
    CarSpritesController spriteController;

    [SerializeField]
    //this will give access to the path script
    Paths paths;

    //this will get all paths to follow
    Transform[] pathsToFollow;

    //this will get the npc speed (random range from the speed range)
    [SerializeField]
    float speed;

    //this will get the current path
    [SerializeField]
    int currentPath;

    int startingPath;

    // this will be true if the player has reached the end of the path
    bool reversePath = false;

    //this is true if the npc needs to stop
    public bool pauseNpc = false;

    //this will get the current rigidbody
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startingPath = currentPath;
        initialPos = this.transform.position;
        //save this object rigidbody in the rb variable
        rb = this.GetComponent<Rigidbody2D>();
        //this will assign a random car image to the gameobject
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteController.ReturnRandomCar(); 
        //get the paths
        pathsToFollow = paths.paths;
    }

    private void Update()
    {
        
        //if its not reversed path, do the normal path
        if (reversePath == false)
        {
            MoveNormalPath();
        }
        //call the coroutine to check if the ai car is mooving
        StartCoroutine(CheckIfIsMoving());

    }

    private IEnumerator CheckIfIsMoving()
    {
        //get the oldPosition
        Vector2 oldPos = this.transform.position;
        //return after 1 second
        yield return new WaitForSeconds(1f);
        
        //if the distance between before and now is less the 0.1
        if (Vector2.Distance(oldPos, this.transform.position)<0.5)
        {
            //if this object is outside screen
            if(CheckIfIsOutsideScreen(this.gameObject))
            {
                this.gameObject.transform.position = initialPos;
                currentPath = startingPath;
            }        
        }
    }

    bool CheckIfIsOutsideScreen(GameObject objectToCheck)
    {
        //if the current object is not in the camera return true, else false
        if (!objectToCheck.GetComponent<Renderer>().isVisible) return true;
        else return false;
        
    }
   

    void MoveNormalPath()
    {
        //this.transform.position.x == pathsToFollow[currentPath].position.x && this.transform.position.y == pathsToFollow[currentPath].position.y
        //if the npc is on the same pos that the path
        if (Vector2.Distance(this.transform.position, pathsToFollow[currentPath].position) < 0.1)
        {
            //if we are at the last position
            if (currentPath + 1 >= pathsToFollow.Length)
            {

                currentPath = 0;
            }
            else
            {
                if (paths.skipablePoints.Length != 0)
                {

                    //do a random to randomize if it goes trought the loop of skipping points, if the path has one
                    int randomreverse = Random.Range(1, 3);
                    //if the randomizer wants the car to check if theres a skipable point
                    if (randomreverse == 2)
                    {
                        //do a for to go trhough all skipable points
                        for (int i = 0; i < paths.skipablePoints.Length; i++)
                        {
                            //if the current path is the same as a skipable point, make it skip, i do -1 because below ill do a +1
                            if ((currentPath) == paths.skipablePoints[i].x)
                            {
                                currentPath = paths.skipablePoints[i].y - 1;
                            }
                        }
                    }
                }
                //go to the next position if we are not at the last position
                currentPath = currentPath + 1;
            }
        }

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (pauseNpc == false)
        {
            //move the npc to the current path
            transform.position = Vector2.MoveTowards(transform.position, pathsToFollow[currentPath].position, Time.deltaTime * speed);
            transform.up = pathsToFollow[currentPath].position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.name + " collided with " + collision.gameObject.name);
    }
}

