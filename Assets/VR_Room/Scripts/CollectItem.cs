using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;


public class CollectItem : MonoBehaviour
{
    public bool HaveToReleaseItem;
    public Image[] images;
    public Image[] images_check;
    public GameObject[] target_gameobjects;

    public AudioSource m_audio_correct;
    public AudioSource m_audio_wrong;

    public TextMeshProUGUI Text_ResponseToAnswer;

    private int cnt;

    void Start()
    {
        foreach (var item in images)
            item.gameObject.SetActive(false);
        foreach (var item in images_check)
            item.gameObject.SetActive(false);
        Text_ResponseToAnswer.text = "";
        cnt = 0;
    }

    // can't use onCollisionEnter anymore after set ClipBoard to kinematic
    private void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (col.gameObject.name == target_gameobjects[i].name)
            {
                if (HaveToReleaseItem && col.gameObject.GetComponent<XRGrabInteractable>().isSelected)
                    return;
                images[i].gameObject.SetActive(true);
                images_check[i].gameObject.SetActive(true);
                col.gameObject.SetActive(false);
                m_audio_correct.Play();
                cnt++;
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
            if(cnt < images.Length)
                Text_ResponseToAnswer.text = "Correct!";
            else
                Text_ResponseToAnswer.text = "You Win!";
            yield return new WaitForSeconds(8);
        }
        else
        {
            Text_ResponseToAnswer.text = "Try again!";
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