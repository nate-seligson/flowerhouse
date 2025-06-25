using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class HeadController : MonoBehaviour
{
    // Start is called before the first frame update
    public MultiAimConstraint multiAimConstraint;
    public GameObject target;
    public void TurnPlayerHead(Vector3 position)
    {
        multiAimConstraint.gameObject.SetActive(true);
        target.transform.position = position;
    }
    public void Reset()
    {
        multiAimConstraint.gameObject.SetActive(false);
        multiAimConstraint.data.constrainedObject.transform.rotation = Quaternion.identity;
    }
}
