using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShadow : MonoBehaviour
{

    [SerializeField] private GameObject mySource;
    SpriteRenderer mySpriteRenderer;
    float minDistance;
    [SerializeField] float maxDistance;


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = new Vector3(this.transform.position.x, -1.2f, this.transform.position.z);

        maxDistance = 3.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(mySource.transform.position.x, transform.position.y, transform.position.z);

        float actualDistance = Mathf.Abs(this.transform.position.y - mySource.transform.position.y);
        Color tmp = new Color(1f, 1f, 1f, 1 - Mathf.Abs(actualDistance / maxDistance) * 1.5f );
        mySpriteRenderer.color = tmp;
    }

}
