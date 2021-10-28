using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public bool HaveToReleaseItem = true;
    public Image image_answer;
  
    void Start()
    {
        image_answer.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SphereForTest")
        {
            if (HaveToReleaseItem && col.gameObject.transform.Find("Obj_Script").GetComponent<ModifyStatus>().selected)
                return;
            image_answer.gameObject.SetActive(true);
            col.gameObject.SetActive(false);           
        };
    }


}