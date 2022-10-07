using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundScroll : MonoBehaviour
{
    Material myMaterial;
    Vector2 offset;
    private bool stop = false;

    public int xVelocity, yVelocity;
    private float decelerationSpeed = 1f;


    void Awake()
    {
        myMaterial = GetComponent<Renderer>().material; 
    }

    private void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }


    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;

        if (stop) 
        {
            if (offset.x >= 0.2)
            {
                offset.x -= (decelerationSpeed * xVelocity) * Time.deltaTime;
            }
            else offset.x = 0f;
        }
    }

    public void Stop() 
    {
        stop = true;    
        //offset = new Vector2(0, yVelocity);
    }



}
