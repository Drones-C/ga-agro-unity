using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeField : MonoBehaviour
{
    public Transform x1;
    public Transform x2;

    public GameObject Dirt;
    public GameObject[] Crops;
    public List<GameObject> Population = new List<GameObject>();

    private void create_individual(float cap, float water)
    {
        float x = 0;
        float z = 0;
        int g = 0;
        int r = 0;
        GameObject temp;
            
        x = Random.Range(x2.position.x, x1.position.x);
        z = Random.Range(x1.position.z, x2.position.z);
        g = Random.Range(0, Crops.Length);
        r = Random.Range(0, 100);

        Instantiate(Dirt, new Vector3(x, 50, z), new Quaternion(0, 0, 0, 1));
        temp = Instantiate(Crops[g], new Vector3(x, 50, z), new Quaternion(0, 0, 0, 1));
        //mutate
        if (r >= (water / cap) * 100)
            cap += 10;

        temp.GetComponent<Plant>().water_capacity = cap;
        temp.GetComponent<Plant>().water = cap;
        
        Population.Add(temp);
        StartCoroutine(popul(temp));
    }

    private IEnumerator popul(GameObject plant)
    {
        yield return new WaitForSeconds(25);
        GameObject mother = Population[Random.Range(0, Population.Count)];
        
        Destroy(plant, 25);
        Population.Remove(plant);
    }
