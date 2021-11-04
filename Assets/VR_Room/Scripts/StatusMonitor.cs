using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusMonitor : MonoBehaviour
{
    public GameObject Targeted;

    private bool m_selected;
    private bool m_aimed;
    private bool is_kinematic;
    private float speed;

    private Vector3 original_position;
    private Quaternion original_rotation;
    private Vector3 original_scale;

    private void Start()
    {
        m_selected = false;
        m_aimed = false;
        is_kinematic = gameObject.GetComponent<Rigidbody>().isKinematic;
        speed = 1f;        
        original_position = transform.position;
        original_rotation = transform.rotation;
        original_scale = transform.localScale;
        
    }

    private void Update()
    {
        if (m_aimed) {
            Targeted.SetActive(true);
            Targeted.transform.eulerAngles = Camera.main.transform.eulerAngles; 
        }
        else
            Targeted.SetActive(false);
    }

    public void ChangeTo_Aimed()
    {
        m_aimed = true;
    }

    public void ChangeTo_DeAimed()
    {
        m_aimed = false;
    }

    public bool Selected()
    {
        return m_selected;
    }

    public void ChangeTo_Selected()
    {
        m_selected = true;
    }

    public void ChangeTo_DeSelected()
    {
        m_selected = false;
        if(gameObject.activeSelf)
            BackToOriginalStatus();
    }

    public bool InOriginalStatus()
    {
        return 
            (transform.position == original_position &&
            transform.rotation == original_rotation &&
            transform.localScale == original_scale);
    }

    public void BackToOriginalStatus()
    {
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
    }

    IEnumerator GetBack()
    {
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
        }
    }


}
