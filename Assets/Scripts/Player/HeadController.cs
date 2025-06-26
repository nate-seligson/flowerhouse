using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class HeadController : MonoBehaviour
{
    // Start is called before the first frame update
    public MultiAimConstraint multiAimConstraint;
    public GameObject target;

    public GameObject headreset;

    public Vector3 offset;
    Quaternion initialRotation;
    void Start()
    {
        initialRotation = multiAimConstraint.data.constrainedObject.transform.rotation;
    }
    public void TurnPlayerHead(Vector3 position)
    {
        target.transform.position = Vector3.MoveTowards(target.transform.position, position, 0.05f);
    }
    public void Reset()
    {
        target.transform.position = Vector3.MoveTowards(target.transform.position, headreset.transform.position, 0.05f);
    }
}
