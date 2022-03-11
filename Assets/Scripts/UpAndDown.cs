using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public LeanTweenType easeType;

    void OnEnable() 
    {
        float maxHeight = Random.Range(0.3f, 0.8f);
        float loopTime = Random.Range(0.5f, 1.2f);

        LeanTween.moveY(gameObject, maxHeight, loopTime).setLoopPingPong().setEase(easeType);
    }
}
