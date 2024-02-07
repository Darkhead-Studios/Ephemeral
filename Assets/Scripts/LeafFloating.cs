using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeafFloating : MonoBehaviour
{

    public float wind = 1f;
    public GameObject PlayerPrefab;

    void Update()
    { 
        transform.position = new Vector3(transform.position.x + Random.Range((float)(wind - 0.2), wind) * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.y < PlayerPrefab.transform.position.y - 10 ||
            transform.position.y > PlayerPrefab.transform.position.y + 20 ||
            transform.position.x > PlayerPrefab.transform.position.x + 13 ||
            transform.position.x < PlayerPrefab.transform.position.x - 13) 
            Destroy(gameObject);
 
    }
}
