using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScriptno2 : MonoBehaviour
{
    public Vector3 northDirection;
    public Transform player;
    public Quaternion missionDirection;

    public RectTransform northNeedle;
    public RectTransform pickUpNeedle;
    public Transform[] pickupPlaces;

    // Update is called once per frame
    void Update()
    {
        ChangeNorthDirection();
        ChangeMissionDirection();
    }

    void ChangeNorthDirection()
    {
        northDirection.z = player.eulerAngles.y;
        northNeedle.localEulerAngles = northDirection;
    }

    void ChangeMissionDirection()
    {
        for (int i = 0; i < pickupPlaces.Length; i++)
        {
            if (pickupPlaces[i].GetComponent<SpriteRenderer>().enabled)
            {
                Vector3 dir = transform.position - pickupPlaces[i].position;
                missionDirection = Quaternion.LookRotation(dir);
                missionDirection.z = -missionDirection.y;
                missionDirection.x = 0;
                missionDirection.y = 0;
                pickUpNeedle.localRotation = missionDirection * Quaternion.Euler(northDirection);
            }

        }
    }
}
