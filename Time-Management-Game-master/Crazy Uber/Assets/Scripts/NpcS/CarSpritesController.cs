using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpritesController : MonoBehaviour
{
    
    //this will get all the car sprites
    public Sprite[] allCarSprites;

    //this function will return the normal npc sprite and the death sprite
    public Sprite ReturnRandomCar()
    {
        
        //this will get the sprite to return
        int spriteToSend = Random.Range(0, allCarSprites.Length);
        return allCarSprites[spriteToSend];
    }
}
