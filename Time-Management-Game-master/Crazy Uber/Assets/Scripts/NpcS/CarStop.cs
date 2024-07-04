using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStop : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the Car Npc is close to the player stop the carNpc
        if (collision.CompareTag("Player"))
        {
            //stop the Carnpc
            this.GetComponentInParent<CarScript>().pauseNpc = true;
            
        }
        //if the car is close to other npcCar, check if the other car already stopped if not, stop this car ( i check if the other stopped so that both cars dont stop at the same time and no one moves)
        else if(collision.CompareTag("NpcCar"))
        {
            //if the car script is not paused and its not colliding with the trigger, so that it stops if our car is in front of another car and we create a line
            if(collision.GetComponent<CarScript>().pauseNpc==false || collision.isTrigger == false)
            {
                //stop the CAr Npc
                this.GetComponentInParent<CarScript>().pauseNpc = true;
            }
            //i do this so that they igonore the trigger collisions stoping the car
            if(collision.isTrigger == true)
            {
                Debug.Log(collision.gameObject);
                this.GetComponentInParent<CarScript>().pauseNpc = false;
            }

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the collision who left was a player or a npc car make the car move again
        if (collision.CompareTag("Player") || collision.CompareTag("NpcCar"))
        {
            //Move the car
            this.GetComponentInParent<CarScript>().pauseNpc = false;
        }

    }
}
