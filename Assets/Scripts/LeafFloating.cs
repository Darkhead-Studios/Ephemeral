using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafFloating : MonoBehaviour
{

    public float wind = 1f;
    public GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        transform.position = new Vector3(transform.position.x + wind * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.y < player.transform.position.y - 10) Destroy(gameObject);
   
    }
}
