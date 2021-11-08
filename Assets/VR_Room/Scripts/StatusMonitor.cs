using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StatusMonitor : MonoBehaviour
{
    //public GameObject Targeted;
    private GameObject Targeted;

    private bool m_selected;
    private bool m_aimed;
    private bool is_kinematic;
    private float speed;

    private Vector3 original_position;
    private Quaternion original_rotation;
    private Vector3 original_scale;
    private Material originial_material;
    //private Material highlight_material;
    private bool m_JustRelease;

    public Material highlight_material;

    private void Start()
    {
        //Targeted = transform.Find("Targeted").gameObject;
        //if(Targeted)
        //    Targeted.SetActive(false);
        m_selected = false;
        m_aimed = false;
        is_kinematic = gameObject.GetComponent<Rigidbody>().isKinematic;
        speed = 1f;
        original_position = transform.position;
        original_rotation = transform.rotation;
        original_scale = transform.localScale;
        originial_material = GetComponent<Renderer>().material;
        //highlight_material = new Material(Shader.Find("Shader Graphs/FireBoiShader"));
        m_JustRelease = false;
    }

    private void Update()
    {
        if (m_aimed)
        {
            //Targeted.SetActive(true);
            //Targeted.transform.eulerAngles = Camera.main.transform.eulerAngles;
            GetComponent<Renderer>().material = highlight_material;
        }
        else
        {
            //Targeted.SetActive(false);
            GetComponent<Renderer>().material = originial_material;
        }
        if (GetComponent<XRGrabInteractable>().isSelected)
            m_selected = true;
        else
        {
            if (m_selected)
                BackToOriginalStatus();
            m_selected = false;
        }

    }

    public void ChangeTo_Aimed()
    {
        m_aimed = true;
    }

    public void ChangeTo_DeAimed()
    {
        m_aimed = false;
    }

    public bool JustRelease()
    {
        return m_JustRelease;
    }
    
    //public void ChangeTo_Selected()
    //{
    //    m_selected = true;
    //}

    //public void ChangeTo_DeSelected()
    //{
    //    m_selected = false;
    //    if (gameObject.activeSelf)
    //        BackToOriginalStatus();
    //}

    public bool InOriginalStatus()
    {
        return
            (transform.position == original_position &&
            transform.rotation == original_rotation &&
            transform.localScale == original_scale);
    }

    public void BackToOriginalStatus()
    {
        if (!gameObject.activeSelf)
            return;
        if (!is_kinematic)
        {
            transform.position = original_position;
            transform.rotation = original_rotation;
            transform.localScale = original_scale;
        }
        else
        {
            StartCoroutine(GetBack());
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = is_kinematic;
    }

    IEnumerator GetBack()
    {
        m_JustRelease = true;
        Vector3 position_transit;
        Quaternion rotation_transit;
        Vector3 scale_transit;
        float speedGetBack = speed * Time.deltaTime;
        while (true)
        {
            position_transit = Vector3.MoveTowards(transform.position, original_position, speedGetBack);
            rotation_transit = Quaternion.Lerp(transform.rotation, original_rotation, speedGetBack);
            scale_transit = Vector3.MoveTowards(transform.localScale, original_scale, speedGetBack);
            if (transform.position == position_transit && transform.rotation == rotation_transit && transform.localScale == scale_transit)
                break;
            transform.position = position_transit;
            transform.rotation = rotation_transit;
            transform.localScale = scale_transit;
            yield return null;
            m_JustRelease = false;
        }
    }


}
