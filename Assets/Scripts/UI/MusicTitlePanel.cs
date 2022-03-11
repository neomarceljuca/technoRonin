using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicTitlePanel : MonoBehaviour
{

    
    private string playingSong;
  
    
 
    void Start()
    {
        playingSong = AudioManager.instance.playingSoundTrack.name.ToString();

        gameObject.GetComponentInChildren<TextMeshProUGUI>().text += playingSong;
    }

    private void Awake()
    {
        gameObject.SetActive(true);
        Vector3 initialPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        LeanTween.move(gameObject.GetComponent<RectTransform>(), initialPos, 0f);
        LeanTween.move(gameObject.GetComponent<RectTransform>(), initialPos + new Vector3(305f, 0f,0f), 1f).setDelay(3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.move(gameObject.GetComponent<RectTransform>(), initialPos, 1f).setDelay(7f).setEase(LeanTweenType.easeInQuad);
    }

}
