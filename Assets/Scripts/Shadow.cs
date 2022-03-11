using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    [SerializeField] private GameObject mySource;
    SpriteRenderer mySpriteRenderer;
    float initDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector3(transform.position.x, -1.2f, transform.position.z);
        initDistance = Mathf.Abs(transform.position.y - mySource.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate() 
    { 
        float actualDistance = Mathf.Abs(transform.position.y - mySource.transform.position.y);
        Color tmp = new Color(1f, 1f, 1f, initDistance /actualDistance);
        mySpriteRenderer.color = tmp;
    }
    
}
