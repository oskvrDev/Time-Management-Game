using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    [Header("put the path index to skip, and the to skip to, so if you want to skip from 6 to 8, put 6 and 8")]
    public Vector2Int[] skipablePoints; 
    //this will get every path for this route
    public Transform[] paths;
}
