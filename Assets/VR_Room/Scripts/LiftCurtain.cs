using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCurtain : MonoBehaviour
{
    public RectTransform text_rectTransform;
    public RectTransform text_tip;
    public RectTransform panel_rectTransform;
    public float speed_text;
    public float speed_panel;
    public AudioSource m_audio;

    private bool stopLifting_1;
    //private bool stopLifting_2;

    private void Start()
    {
        stopLifting_1 = false;
        //stopLifting_2 = false;
    }

    void Update()
    {
        if (stopLifting_1)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            return;
        }
        if (text_rectTransform.anchoredPosition.y < 1000)
        {
            text_rectTransform.anchoredPosition += Vector2.up * speed_text * Time.deltaTime;
            if (text_rectTransform.anchoredPosition.y > -400)
                text_tip.gameObject.SetActive(false);
        }
        else
        {
            stopLifting_1 = true;
        }
        if (text_rectTransform.anchoredPosition.y >= -300 && panel_rectTransform.anchoredPosition.y < 700)
            panel_rectTransform.anchoredPosition += Vector2.up * speed_panel * Time.deltaTime;
        else
        {
            //stopLifting_2 = true;
        }

    }

    private void OnDisable()
    {
        m_audio.Play();
    }

}
