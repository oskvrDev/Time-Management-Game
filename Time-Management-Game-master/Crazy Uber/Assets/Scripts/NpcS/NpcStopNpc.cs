using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStopNpc : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the car is close to the npc
        if (collision.CompareTag("Player"))
        {
            //stop the npc
            this.GetComponentInParent<NpcFollowPath>().pauseNpc = true;
           
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the car is close to the npc
        if (collision.CompareTag("Player"))
        {
            //stop the npc
            this.GetComponentInParent<NpcFollowPath>().pauseNpc = false;
        }

    }

}
