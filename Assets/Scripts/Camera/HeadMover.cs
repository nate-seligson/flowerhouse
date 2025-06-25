using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMover : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)){
            Debug.Log(hit.transform.gameObject.tag);
            if(hit.transform.gameObject.tag == "wall"){
                target.transform.position = hit.point;
            }

        }
    }
}
