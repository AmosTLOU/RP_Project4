using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistanceGrab_Customed : MonoBehaviour
{
    public Transform controllerTransform;
    public float maxDistance;
    public LineRenderer beam;
    public AudioSource audio_summon;
    public AudioSource audio_release;

    private GameObject ObjAimed;
    private GameObject ObjSummoned;
    private bool holdingItem;
    private int counter;

    private void Start()
    {
        ObjAimed = null;
        counter = 0;
        holdingItem = false;
    }

    private void Update()
    {
        if (ObjSummoned)
        {
            //Debug.Log("ObjSummoned exists");
            int FramesToStay = 3 * (int)(1f / Time.deltaTime);
            if (counter >= FramesToStay && ObjSummoned && !ObjSummoned.GetComponent<XRGrabInteractable>().isSelected)
            {
                ObjSummoned.GetComponent<StatusMonitor>().BackToOriginalStatus();
                ObjSummoned = null;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else
        {
            //Debug.Log("ObjSummoned is null");
        }

        if (ObjSummoned && ObjSummoned.GetComponent<StatusMonitor>().JustRelease())
        {
            audio_release.Play();
        }
    }

    public void Detect()
    {
        if (holdingItem)
        {
            DeDetect();
            return;
        }            
        Vector3 startPoint = controllerTransform.position;
        Vector3 drt = controllerTransform.forward;
        Ray ray = new Ray(startPoint, drt);
        if (!ObjAimed || !ObjAimed.GetComponent<XRGrabInteractable>().isSelected)
        {
            beam.SetPosition(0, startPoint);
            beam.SetPosition(1, startPoint + drt * maxDistance);
        }
        RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log("Distance Grab, Detect Object: " + hit.collider.name);
            if (ObjAimed)
            {
                ObjAimed.GetComponent<StatusMonitor>().ChangeTo_DeAimed();
                ObjAimed = null;
            }
            if (hit.collider.gameObject.layer == 8)
            {
                ObjAimed = hit.collider.gameObject;
                ObjAimed.GetComponent<StatusMonitor>().ChangeTo_Aimed();
            }
        }
        else
        {
            if (ObjAimed)
            {
                ObjAimed.GetComponent<StatusMonitor>().ChangeTo_DeAimed();
                ObjAimed = null;
            }
        }
    }

    public void DeDetect()
    {
        beam.SetPosition(0, Vector3.zero);
        beam.SetPosition(1, Vector3.zero);
        if (ObjAimed)
        {
            ObjAimed.GetComponent<StatusMonitor>().ChangeTo_DeAimed();
            ObjAimed = null;
        }

    }

    public void Summon()
    {
        if (ObjAimed && !ObjSummoned)
        {
            audio_summon.Play();
            ObjAimed.GetComponent<Rigidbody>().isKinematic = true;
            ObjAimed.GetComponent<StatusMonitor>().ChangeTo_DeAimed();
            //ObjAimed.transform.position = controllerTransform.parent.TransformPoint(controllerTransform.localPosition + new Vector3(-0.1f, 0.1f, 0f));
            ObjAimed.transform.position = controllerTransform.position;
            ObjSummoned = ObjAimed;
            ObjAimed = null;
            counter = 0;
        }
    }

    public void HoldingItem()
    {
        holdingItem = true;
    }

    public void ReleaseItem()
    {
        holdingItem = false;
    }

}
