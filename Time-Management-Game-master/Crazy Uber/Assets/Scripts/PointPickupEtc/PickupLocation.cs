using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().color ==Color.red)
        {
            Debug.Log("AADSSADSAD");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //collision.GetComponent<CarController>().
        }
    }
}
