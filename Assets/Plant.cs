using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plant : MonoBehaviour
{
    public float water;
    public float water_capacity;
    public List<int> Genome = new List<int>();

    private IEnumerator MySecUpdater()
    {
        yield return new WaitForSeconds(1);
        water--;
        if(water<=0)
            Destroy(gameObject);
    }

