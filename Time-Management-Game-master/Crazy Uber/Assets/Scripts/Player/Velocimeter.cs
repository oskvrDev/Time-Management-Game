using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Velocimeter : MonoBehaviour
{
    //sthis script was inspired by the video: https://www.youtube.com/watch?v=CC8j_fU2GTQ&list=PLSr3T4Kxh2HHiaryizw71-Eg6pF-uSOm_&index=29 
    [SerializeField]
    //this will get the number that needs to be multiplyed for it to be able to get real kms 
    float KmsToMultiply;
    [SerializeField]
    //this will get the car rigidbody
    Rigidbody2D carRigidBody;
    [SerializeField]
    //this wil get the max speed of the car
    float maxCarSpeed = 0.0f; 
    [SerializeField]
    //this will get the minimal angle of the arrow, (z rotation of the pointer image)
    float minSpeedArrowAngle;
    [SerializeField]
    //this will get the angle of the arrow when the car is at full speed, (z rotation of the pointer image)
    float maxSpeedArrowAngle;

    [Header("UI Objects")]
    [SerializeField]
    //this will get the speed text
    Text speedLabel;
    [SerializeField]
    /*this will get the recttransform of the speed pointer image, a recttransform will get  the transform of the image,
     * if you go to the inspector, youll see that the speed pointer has a rect transform, because hes an ui image, so like this 
     * we can have the information inside that, that is position, scale, rotation, anchors, pivot.*/
    RectTransform arrow; // The arrow in the speedometer
    //this will get the current car speed
    private float currentCarSpeed = 0.0f;
    private void Update()
    {
        // get the car speed, then multiply kmstomultiply, to get a realistic number
        currentCarSpeed = carRigidBody.velocity.magnitude * KmsToMultiply;

        //if we have a speed label
        if (speedLabel != null)
        {
            //this will change the label that says the speed, to the car current speed (converted to an int, so that it doesnt be a number like 20,531)
            speedLabel.text = ((int)currentCarSpeed) + " km/h";
        }
        //if we have a arrow
        if (arrow != null)
        {
            
            //change the arrow rotation, to move between the minimal arrwo angle and the max speed angle (from 0 to 260), using the car current speed
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, currentCarSpeed / maxCarSpeed));
        }
    }
}
