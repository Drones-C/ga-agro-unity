using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position,Vector3.up, Time.deltaTime*9000);
    }
}
