using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpritesController : MonoBehaviour
{
    [Header("the npcs and the death npcs" +
        " NEEDS to be the same, if the blonde" +
        " girl is number 1, the death blonde" +
        " girl NEEDS to be 1 aswell")]
    //this will get all the npc sprites
    public Sprite[] allNpcsSprites;
    //this will get all the npc death Sprites
    public Sprite[] allDeathNpcsSprites;


    //this function will return the normal npc sprite and the death sprite
    public Sprite[] ReturnRandomNpc()
    {
        //this will create an array that will save the npc and the respetive death npc
        Sprite[] NpcAndDeathNpc = new Sprite[2];
        //this will get the sprite to return
        int spriteToSend=Random.Range(0, allNpcsSprites.Length);
        //say what npc to use
        NpcAndDeathNpc[0] = allNpcsSprites[spriteToSend];
        //say what death npc to use
        NpcAndDeathNpc[1] = allDeathNpcsSprites[spriteToSend];
        //return both sprites
        return NpcAndDeathNpc;
    }
}
