using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject TheDrone;
    private short speed;

    private void Flight(Vector3 flight_vector)
    {
        transform.Translate(flight_vector,Space.Self);
    }

    private IEnumerator Rotation(short dir = 1)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TheDrone.transform.Rotate(Vector3.forward * dir,Time.deltaTime * 15);

        if(TheDrone.transform.rotation.z < 0.14)
            StartCoroutine(Rotation());
    }

    private IEnumerator TakeOff()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Vector3 flight_vector = new Vector3(0,Time.deltaTime * speed,0);
        Flight(flight_vector);

        if (transform.position.y < 60)
            StartCoroutine(TakeOff());
        else
        {
            speed = 13;
            StartCoroutine(Rotation());
            StartCoroutine(DroneLoop());
        }
    }

    private IEnumerator DroneLoop()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Vector3 flight_vector;
        
        if(transform.position.x > 283)
        {
            flight_vector = new Vector3(Time.deltaTime * -speed,0,0);
            Flight(flight_vector);
        }
        
        StartCoroutine(DroneLoop());
    }
    
    private void Start()
    {
        speed = 5;
        StartCoroutine(TakeOff());
    }
}
