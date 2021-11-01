using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ClipBoardControl : MonoBehaviour
{
    public Transform LeftControllerTransform;
    public Transform camTransform;
    public Vector3 dst_from_lefthand;

    private Vector3 original_rotation;

    private void Start()
    {
        original_rotation = this.transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if(transform.position.y < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Appear()
    {
        this.transform.position = LeftControllerTransform.parent.TransformPoint(LeftControllerTransform.localPosition - dst_from_lefthand);
        float y = camTransform.rotation.eulerAngles.y;
        this.transform.rotation = Quaternion.Euler(original_rotation.x, original_rotation.y + y, original_rotation.z) ;
    }
}
