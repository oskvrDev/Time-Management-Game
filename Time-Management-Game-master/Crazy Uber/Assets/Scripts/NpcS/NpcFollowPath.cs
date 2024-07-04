using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFollowPath : MonoBehaviour
{
    //this will  get the sprite controllers
    [SerializeField]
    NpcSpritesController spriteController;
    //this will create an array that will save the npc sprite (0) and the respetive death npc sprite (1)
    Sprite[] NpcAndDeathNpc = new Sprite[2];


    //this will get the initial position of the npc, so that if he dies, he spawns, back 
    Vector2 initPos;
    [SerializeField]
    //this will give access to the path script
    Paths paths;

    //this will get all paths to follow
    Transform[] pathsToFollow;

    [SerializeField]
    //this will be the speed of the npc
    Vector2 speedRange;

    //this will get the npc speed (random range from the speed range)
    private float speed;

    //this will get the current path
    int currentPath;

    //this bool is true if the player died
    bool isDead = false;

    [SerializeField]
    //if this is true, is because the path is a loop
    bool isLoop = false;

    // this will be true if the player has reached the end of the path
    bool reversePath = false;

    //this is true if the npc needs to stop
    public bool pauseNpc = false;

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        //get the npc sprite (0) and death npc sprite (1)
        NpcAndDeathNpc = spriteController.ReturnRandomNpc();
        //change the npc sprite

        this.GetComponent<SpriteRenderer>().sprite = NpcAndDeathNpc[0];

        //get the initial position of the npc
        initPos = this.transform.position;
        //if the path is a loop let him reverse if the randomizer wants to
        if(isLoop==true)
        {
            //do a random if is 2 is because it will be a reverse path
            int randomreverse = Random.Range(1, 3);
            //if the random range is 2, just reverse the path
            if (randomreverse == 2)
            {
                reversePath = true;
            }
            else
            {
                reversePath = false;
            }

        }

        //get the paths
        pathsToFollow = paths.paths;   
        // create a speed range for the npcs to not have all the same speed
        speed = Random.Range(speedRange.x, speedRange.y);
    }



    private void Update()
    {
        
        //if its not reversed path, do the normal path
        if (reversePath == false)
        {
            MoveNormalPath();
        }
        //if its the reversed path do the reversed path
        else
        {
            MoveReversePath();
        }
    }

    void MoveNormalPath()
    {
      
        //if the npc is really close to the path
        if (Vector2.Distance(this.transform.position, pathsToFollow[currentPath].position) < 0.5f)
        {
            //if we are at the last position
            if (currentPath + 1 >= pathsToFollow.Length)
            {
                //go to the first position because we are at the last position
                if(isLoop==true)
                {
                    currentPath = 0;
                }
                else
                {
                    reversePath = true;
                }
            }
            else
            {
                //go to the next position if we are not at the last position
                currentPath = currentPath + 1;
            }
        }
        
    }

    void MoveReversePath()
    {
      
        //if the npc is really close to the path
        if (Vector2.Distance(this.transform.position, pathsToFollow[currentPath].position) < 0.5f)
        {
            //if we are at the first position
            if (currentPath - 1 < 0)
            {
                //go to the first position because we are at the last position
                if (isLoop == true)
                {
                    //go to the last pos (we are doing the path reversed
                    currentPath = pathsToFollow.Length - 1;
                }
                else
                {
                    reversePath = false;
                }
                              
            }
            else
            {
                //go to the path before
                currentPath = currentPath - 1;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDead==false && pauseNpc==false)
        {
            //move the npc to the current path
            transform.position = Vector2.MoveTowards(transform.position, pathsToFollow[currentPath].position, Time.deltaTime * speed);
            transform.up = pathsToFollow[currentPath].position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the car runned him over
        if(collision.CompareTag("Player"))
        {
            //say that the npc died
            isDead = true;
            //deactivate the box collider so that the player only loses 1 star
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //change the sprite of the player to the death one
            this.GetComponent<SpriteRenderer>().sprite = NpcAndDeathNpc[1];
            StartCoroutine(WaitThenDie());
        }
        
    }

    IEnumerator WaitThenDie()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        //Respawn the player
        this.transform.position = initPos;
        //remove dead sprite
        this.GetComponent<SpriteRenderer>().sprite = NpcAndDeathNpc[0];
        // and say hes not dead anymore
        isDead = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;


    }

}
