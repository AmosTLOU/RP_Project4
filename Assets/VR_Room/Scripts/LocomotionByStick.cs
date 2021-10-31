using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionByStick : MonoBehaviour
{
    public float speed = 0.8f;
    public Transform xrRigTransform;
    public Transform camTransform;
    public Transform headsetTransform;
    public Transform ControllerOffsetTransform;
        
  
    void Update()
    {        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if(moveHorizontal != 0 || moveVertical != 0)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            float y = camTransform.rotation.eulerAngles.y;
            movement = Quaternion.Euler(0, y, 0) * movement;
            camTransform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        camTransform.position = new Vector3(camTransform.position.x, headsetTransform.position.y, camTransform.position.z);
        ControllerOffsetTransform.position = camTransform.position - headsetTransform.position + xrRigTransform.position;
        //ControllerOffsetTransform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
