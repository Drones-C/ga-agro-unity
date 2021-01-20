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

    void Start()
    {
        StartCoroutine(MySecUpdater());
        int cap = Random.Range(5, 10);
        water_capacity = cap;
        water = cap;
        for (int i = 0; i < 20; i++)
            Genome.Add(Random.Range(0, 2));
    }
