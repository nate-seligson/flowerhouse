using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    
    public static bool readyToSpawn = true;
    void Update()
    {
        if (readyToSpawn)
        {
            readyToSpawn = false;
            Instantiate(customer);
        }
    }
}
