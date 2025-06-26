using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public float radius = 15f;

    float minDistance;

    Collider[] hits;

    Collider closestCollider;
    HeadController headController;
    void Start()
    {
        minDistance = radius; // initializer   
        headController = GetComponent<HeadController>();
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
            headController.TurnPlayerHead(closestCollider.transform.position);
        }
        else
        {
            headController.Reset();
        }
    }
}
