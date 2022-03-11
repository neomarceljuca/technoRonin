using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectHitSign : MonoBehaviour
{
    Color opaque;
    Image myImage;
    Vector3 initialPos;

    public void Start()
    {
        opaque = new Color(255, 255, 255, 1);
        myImage = GetComponent<Image>();
        initialPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    public void Display() 
    {
        
        LeanTween.move(gameObject.GetComponent<RectTransform>(), initialPos, 0f);
        myImage.color = opaque  ;
        LeanTween.alpha(myImage.rectTransform, 0, .25f);
        LeanTween.move(gameObject.GetComponent<RectTransform>(), initialPos + new Vector3(0f, 100f, 0f), 1f).setEase(LeanTweenType.easeOutQuad);
    }
}
