using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : MonoBehaviour
{
    float cooldownTime;
    public Player coolDownSource;
    float nextActionTime = 0f;

    void Start()
    {
        cooldownTime = 1/ coolDownSource.attackRate; 
    }

    public void  resetCooldown() 
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + cooldownTime;
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0f);
            LeanTween.scaleY(gameObject, 0, cooldownTime);
        }
            
    }


}
