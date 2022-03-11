using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDirectionAndSpin : MonoBehaviour
{
    float randX;
    float randY;
    float randZ;
    void Start()
    {
        randX = Random.Range(-400f, 400f);
        randY = Random.Range(0f, 400f);
        randZ = Random.Range(-10f, 10f);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(randX, randY));
        
        Destroy(this.gameObject, 1f);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, randZ);
    }

}
