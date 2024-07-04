using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineDebug : MonoBehaviour
{
    [SerializeField]
    Transform[] transformsToDrawLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<transformsToDrawLine.Length-1; i++)
        {
            Debug.DrawLine(transformsToDrawLine[i].position, transformsToDrawLine[i+1].position, Color.red);
        }

    }
}
