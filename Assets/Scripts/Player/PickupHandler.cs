using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    ObjectDetection objectDetector;
    GameObject held_object;
    public float throw_amt = 50;
    public Transform holdspot;
    GameObject just_held; // to stop accidental pickup
    float just_held_timer = 0.5f; // how long before an object can be picked up again if it was just held

    Vector3 offset;
    void Start()
    {
        objectDetector = GetComponent<ObjectDetection>();
    }
    void Update()
    {
        //if not holding anything
        if (held_object == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objectDetector.closestCollider == null)
                {
                    return;
                }
                Pickup(objectDetector.closestCollider.gameObject);
            }
        }
        //if holding something
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
            }
        }
    }
    Vector3 CalculateOffset(GameObject obj)
    {
        Vector3 size = obj.GetComponent<Renderer>().bounds.size;
        return new Vector3(size.x / 2, size.y / 2 , 0);
    }
    void Pickup(GameObject pickup_object)
    {
        //edge case where two quickly dropped objects overload
        if (just_held != null)
        {
            just_held.tag = "pickup";
            just_held = null;
        }
        StopCoroutine("just_held_cooldown");

        held_object = pickup_object;

        Rigidbody rb = held_object.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        held_object.GetComponent<Collider>().enabled = false;

        offset = CalculateOffset(held_object);

        held_object.transform.position = holdspot.transform.position + transform.up * offset.y;
        held_object.transform.rotation = transform.rotation;
        held_object.transform.parent = holdspot;
    }
    void Drop()
    {
        Rigidbody rb = held_object.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        held_object.GetComponent<Collider>().enabled = true;

        held_object.transform.parent = null;

        rb.AddForce(transform.right * throw_amt, ForceMode.Impulse); //for some reason right is forward? smh

        //handle cooldown
        if (just_held == null)
        {
            StartCoroutine("just_held_cooldown");
        }

        held_object = null;
    }
    IEnumerator just_held_cooldown()
    {
        just_held = held_object;
        just_held.tag = "Untagged";

        yield return new WaitForSeconds(just_held_timer);

        just_held.tag = "pickup";
        just_held = null;
    }
}
