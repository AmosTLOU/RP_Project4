using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCurtain : MonoBehaviour
{
    public RectTransform text_rectTransform;
    public RectTransform panel_rectTransform;
    public float speed_text;
    public float speed_panel;

    void Update()
    {
        if(text_rectTransform.anchoredPosition.y < 1000)
            text_rectTransform.anchoredPosition += Vector2.up * speed_text * Time.deltaTime;
        if(text_rectTransform.anchoredPosition.y >= -300 && panel_rectTransform.anchoredPosition.y < 700)
            panel_rectTransform.anchoredPosition += Vector2.up * speed_panel * Time.deltaTime;

    }
    
}
