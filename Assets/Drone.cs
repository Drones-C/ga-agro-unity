using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public short speed;

    private bool gained_high = false;

    private void Flight(Vector3 flight_vector)
    {
        transform.Translate(flight_vector,Space.Self);
    }

    private IEnumerator Rotation(short dir = 1)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        transform.Rotate(Vector3.forward * dir,Time.deltaTime * 15);

        if(transform.rotation.z < 0.14)
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
            gained_high = true;
    }
    
    private void Start()
    {
        speed = 5;
        StartCoroutine(TakeOff());
    }

    private void LateUpdate()
    {
        if (gained_high)
        {
            //
        }
    }
}
