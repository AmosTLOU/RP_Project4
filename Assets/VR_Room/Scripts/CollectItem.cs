using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CollectItem : MonoBehaviour
{
    public bool HaveToReleaseItem;
    public Image[] images;
    public GameObject[] target_gameobjects;

    public AudioSource m_audio_correct;
    public AudioSource m_audio_wrong;

    public TextMeshProUGUI Text_ResponseToAnswer;

    void Start()
    {
        foreach(var item in images)
            item.gameObject.SetActive(false);
        Text_ResponseToAnswer.text = "";
    }
    
    // can't use onCollisionEnter anymore after set ClipBoard to kinematic
    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (col.gameObject.name == target_gameobjects[i].name)
            {
                if (HaveToReleaseItem && col.gameObject.GetComponent<StatusMonitor>().Selected())
                    return;
                images[i].gameObject.SetActive(true);
                col.gameObject.SetActive(false);
                m_audio_correct.Play();
                StartCoroutine(ShowResponseText(col.gameObject.name));
                return;
            }
        }
        if (col.gameObject.layer == 8)
        {
            m_audio_wrong.Play();
            StartCoroutine(ShowResponseText(""));
        }
            
    }

    IEnumerator ShowResponseText(string name)
    {
        if (name != "")
        {
            Text_ResponseToAnswer.text = name + " is a correct answer!";
            yield return new WaitForSeconds(8);
        }
        else
        {
            Text_ResponseToAnswer.text = "Well... Let's try again!";
            yield return new WaitForSeconds(2);
        }        
        Text_ResponseToAnswer.text = "";

        //int fps = (int)(1f / Time.deltaTime);
        //int cnt = 0;
        //Vector3 original_anchoredPosition3D = Text_ResponseToAnswer.GetComponent<RectTransform>().anchoredPosition3D;
        //for (int i = 0; i < fps; i++)
        //{
        //    if(i % (fps/10) == 0)
        //    {
        //        if ((cnt & 1) == 0)
        //            Text_ResponseToAnswer.GetComponent<RectTransform>().anchoredPosition3D += Vector3.up * 2;
        //        else
        //            Text_ResponseToAnswer.GetComponent<RectTransform>().anchoredPosition3D -= Vector3.up * 2;
        //        cnt++;
        //    }
        //    yield return null;
        //}
        //Text_ResponseToAnswer.text = "";
        //Text_ResponseToAnswer.GetComponent<RectTransform>().anchoredPosition3D = original_anchoredPosition3D;
    }

}