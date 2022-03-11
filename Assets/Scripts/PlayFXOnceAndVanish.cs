using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFXOnceAndVanish : MonoBehaviour
{
    public Animator myAnimator;
    

    void Start()
    {
        
        myAnimator.Play("SlicedBox");
        Destroy(this.gameObject, 0.7f);


    }

    private void FixedUpdate()
    {
        transform.Translate(-2f * Time.deltaTime, 0f, 0f);
    }


    private IEnumerator WaitAndDie(GameObject target)
    {

        while (Time.timeScale != 1.0f)
        {
            yield return null;
        }


        //TODO Instanciar caixa cortada

    }
}
