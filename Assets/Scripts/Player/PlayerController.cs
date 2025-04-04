using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput = 0;
    float verticalInput = 0;

    public Transform camera;
    public float speed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 move;
        float cameraRot = camera.rotation.eulerAngles.y;
        Debug.Log(cameraRot);
        if(cameraRot < 45 || cameraRot > 320){
            move = new Vector3(horizontalInput,0,verticalInput);
        }
        else if(cameraRot < 90 + 45){
            move = new Vector3(verticalInput,0,-horizontalInput);
        } 
        else if(cameraRot < 180 + 45){
            move = new Vector3(-horizontalInput,0,-verticalInput);
            }
        else{
            move = new Vector3(-verticalInput,0,horizontalInput);
        }
        transform.Translate(move * speed * Time.deltaTime);
        
    }
}
