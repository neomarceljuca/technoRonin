using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDie : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 1.2f);
    }
}
