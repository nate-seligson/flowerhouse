using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    //head move target
    public GameObject target;
    public float radius = 15f;

    float minDistance;

    Collider[] hits;

    Collider closestCollider = null;
    void Start()
    {
        minDistance = radius; // initializer   
    }
    void FixedUpdate()
    {
        //reset collider params
        closestCollider = null;
        minDistance = radius;
        hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("pickup"))
            {
                continue;
            }
            float distance = (hit.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closestCollider = hit;
            }
        }

    }
    void Update()
    {
        if (closestCollider != null)
        {
            TurnPlayerHead(closestCollider.transform.position);
        }
        else
        {
            TurnPlayerHead(transform.position);
        }
    }
    //moves player head turn target to desired position
    void TurnPlayerHead(Vector3 position)
    {
        target.transform.position = position;
    }
}
