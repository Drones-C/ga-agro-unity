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

    private IEnumerator BalanceRotation(Vector3 vec, char axis = 'z', short dir = 1)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TheDrone.transform.Rotate(vec * dir,Time.deltaTime * rot_speed);

        if (axis == 'z')
        {
            if(dir == 1 && TheDrone.transform.rotation.z < -rot_angle_balanced
               || dir == -1 && TheDrone.transform.rotation.z > rot_angle_balanced)
                StartCoroutine(BalanceRotation(vec,axis,dir));
        }
        else if (axis == 'x')
        {
            if(dir == 1 && TheDrone.transform.rotation.x < -rot_angle_balanced
               || dir == -1 && TheDrone.transform.rotation.x > rot_angle_balanced)
                StartCoroutine(BalanceRotation(vec,axis,dir));
        }
        else if (axis == 'y')
        {
            if(dir == 1 && TheDrone.transform.rotation.y < -rot_angle_balanced
               || dir == -1 && TheDrone.transform.rotation.y > rot_angle_balanced)
                StartCoroutine(BalanceRotation(vec,axis,dir));
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
            speed = horisontal_speed;
            StartCoroutine(Rotation(Vector3.forward));
            StartCoroutine(DroneLoop());
        }
    }

    private IEnumerator DroneLoop()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Vector3 flight_vector;

        //First line of the field
        if(isAtx1 && transform.position.x > 283)
        {
            flight_vector = new Vector3(Time.deltaTime * -speed,0,0);
            Flight(flight_vector);
        }
        else if(!isAtx2)
        {
            StartCoroutine(BalanceRotation(Vector3.forward,'z',-1));
            StartCoroutine(Rotation(Vector3.right,'x',1));
            isAtx1 = false;
            isAtx2 = true;
        } 
        
        //Second line of the field
        if(isAtx2 && transform.position.z < 412)
        {
            flight_vector = new Vector3(0,0,Time.deltaTime * speed);
            Flight(flight_vector);
        }
        else if(!isAtx3)
        {
            isAtx2 = false;
            isAtx3 = true;
        } 
        
            
        
        StartCoroutine(DroneLoop());
    }
    
    private void Start()
    {
        speed = vertical_speed;
        StartCoroutine(TakeOff());
    }
}
