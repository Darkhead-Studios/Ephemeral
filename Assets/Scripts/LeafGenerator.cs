using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafGenerator : MonoBehaviour
{
    private float counter = 0;

    public int timer = 1;
    public GameObject LeafPrefab;
    public GameObject PlayerPrefab;

    void Start()
    {
        GenerateLeaf();
    }

    void Update()
    {
        if (!(transform.position.y < PlayerPrefab.transform.position.y - 15 ||
            transform.position.y > PlayerPrefab.transform.position.y + 25 ||
            transform.position.x > PlayerPrefab.transform.position.x + 15 ||
            transform.position.x < PlayerPrefab.transform.position.x - 15))
        {
            if (counter < timer) counter += Time.deltaTime;
            else
            {
                counter = 0;
                GenerateLeaf();
            }
        }
        
    }
        

    void GenerateLeaf()
    {
        Vector3 spawn = new Vector3(
            transform.position.x + Random.Range(-3, 3),
            transform.position.y + Random.Range(0, 3), 
            transform.position.z);
        Instantiate(LeafPrefab, spawn, transform.rotation);
    }
}
