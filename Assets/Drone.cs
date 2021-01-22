using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject TheDrone;
    private short speed;
    private short vertical_speed = 5;
    private short horisontal_speed = 13;
    private short rot_speed = 45;
    private float rot_angle = 0.13f;
    private float rot_angle_balanced = 0.01f;

    private bool isAtx1 = true;
    private bool isAtx2 = false;
    private bool isAtx3 = false;
    private bool isAtx4 = false;

    private void Flight(Vector3 flight_vector)
    {
        transform.Translate(flight_vector,Space.Self);
    }

    private IEnumerator Rotation(Vector3 vec, char axis = 'z', short dir = 1)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TheDrone.transform.Rotate(vec * dir,Time.deltaTime * rot_speed);

        if (axis == 'z')
        {
            if(dir == 1 && TheDrone.transform.rotation.z < rot_angle
               || dir == -1 && TheDrone.transform.rotation.z > -rot_angle)
                StartCoroutine(Rotation(vec,axis,dir));
        }
        else if (axis == 'x')
        {
            if(dir == 1 && TheDrone.transform.rotation.x < rot_angle
               || dir == -1 && TheDrone.transform.rotation.x > -rot_angle)
                StartCoroutine(Rotation(vec,axis,dir));
        }
        else if (axis == 'y')
        {
            if(dir == 1 && TheDrone.transform.rotation.y < rot_angle
               || dir == -1 && TheDrone.transform.rotation.y > -rot_angle)
                StartCoroutine(Rotation(vec,axis,dir));
        }
    }
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
