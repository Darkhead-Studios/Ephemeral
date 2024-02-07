using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafGenerator : MonoBehaviour
{
    private float counter = 0;
    public int timer = 5;
    public GameObject leaf;

    void Start()
    {
        GenerateLeaf();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < timer) counter += Time.deltaTime;
        else
        {
            counter = 0;
            GenerateLeaf();
        }
        
    }

    void GenerateLeaf()
    {
        Vector3 spawn = new Vector3(
            transform.position.x + Random.Range(-3, 3),
            transform.position.y + Random.Range(0, 3), 
            transform.position.z);
        Instantiate(leaf, spawn, transform.rotation);
    }
}
