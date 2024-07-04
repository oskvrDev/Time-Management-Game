using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{   
    public Transform player;
    
    public Transform[] locations;
    public Transform compass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMissionDirection();
    }

    void ChangeMissionDirection()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].GetComponent<SpriteRenderer>().enabled)
            {
                Vector3 direction = (locations[i].position - player.position).normalized;
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                compass.transform.rotation = Quaternion.Euler(0f, 0f, -targetAngle);

                /*Vector3 dir = (player.position - locations[i].position).normalized;

                Vector3 newDirection = Vector3.RotateTowards(compass.transform.forward, dir, rotationSpeed * Time.deltaTime, 0.0f);
                
                compass.rotation = Quaternion.LookRotation(newDirection);*/
            }
            
        }      

    }
}
