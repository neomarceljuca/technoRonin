using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    SpriteRenderer rend;
    public float movementHSpeed;
    public Shader blankShader;
    public ParticleSystem myParticles;
    public Rigidbody2D myRigidbody2D;
    private Collider2D myCollider;
    public GameObject SlicedObject;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
        movementHSpeed = -movementHSpeed;
        myRigidbody2D.velocity += new Vector2(movementHSpeed, 0);
    }

    void FixedUpdate()
    {
        if (transform.position.x <= -5) Destroy(transform.parent.gameObject);
    }


    public void turnBlank() 
    {
        rend.material.shader = blankShader;
    }

    
    public void ThrowParticles() 
    {
        Vector3 myPos = new Vector3(transform.position.x, transform.position.y); 
        if (myParticles != null)
        {
            Instantiate(myParticles, myPos, Quaternion.LookRotation(Vector3.forward));
        }

        if (SlicedObject != null) {
            GameObject slicedBox = Instantiate(SlicedObject, myPos, Quaternion.identity);
            slicedBox.GetComponent<Rigidbody2D>().velocity = myRigidbody2D.velocity;
        }    
    }

    //change layer of object to default to avoid any colisions (from attacks) after death
    public void ignoreCollisions(Collider2D col) 
    {  
        myCollider.isTrigger = true;
        transform.parent.gameObject.layer = 0;
    }
}
