using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRaceLine : MonoBehaviour
{
    //This script had the help from https://www.youtube.com/watch?v=5ZBynjAsfwI

    //this will get the line renderer component
    LineRenderer lineRenderer;
    //this will get all the points
    public Transform[] points;

    private void Awake()
    {
        //get the line renderer component
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        //call the function to set up the points
        SetUpLine(points);
    }

    public void SetUpLine(Transform[] points_)
    {
        //say how many points the line renderer has
        lineRenderer.positionCount = points.Length;
        //this currently does nothing, because the points variable is already filled, but in case we want to call this script from outside we do this
        points = points_;
        //do a for loop positionning all of the points
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    
}
