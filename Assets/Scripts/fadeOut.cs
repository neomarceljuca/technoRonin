using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour
{
    public fadeOut(float duration)
    {
        LeanTween.alpha(gameObject, 0, duration);
    }

}
