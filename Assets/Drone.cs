using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up / 25,Space.Self);
    }
}
