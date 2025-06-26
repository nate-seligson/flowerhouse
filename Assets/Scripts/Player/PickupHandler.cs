using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    GameObject closestObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("HI");
        }
    }
}
