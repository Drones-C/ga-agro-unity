using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Vector3 flight_vector;
    public short speed;

    private void Start()
    {
        speed = 5;
    }

    void Update()
    {
        //Takeoff
        if (transform.position.y < 60)
        {
            flight_vector = new Vector3(0,Time.deltaTime * speed,0);
        }
        else
        {
            flight_vector = Vector3.zero;   
        }
        
        
        //Flight
        transform.Translate(flight_vector,Space.Self);
    }
}
