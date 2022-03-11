using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField]private GameObject crate;

    public GameObject[] SpawnableObjects;

    
    public float maxTime;
    public float minTime;
    //current time
    private float time;

    //The time to spawn the object
    private float spawnTime;

    void Start()
    {
        SetRandomTime();
        time = 0;


        maxTime = 4f;
        minTime = 1.5f;
        StartCoroutine(increaseSpeedEveryNSeconds(5));
    }

    void FixedUpdate()
    {
        //Counts up
        time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (time >= spawnTime)
        {
            spawnStuff(SpawnableObjects[Random.Range(0,SpawnableObjects.Length)]);
            SetRandomTime();
        }
    }

    GameObject spawnStuff( GameObject spawningPrefab ) 
    {
        Vector3 spawnPos;
        GameObject thisInstance;
        if (spawningPrefab.name.Equals("Crate"))
        {
            spawnPos = new Vector3(transform.position.x, Random.Range(-0.5f, 1f), 0f);

            time = 0;
            thisInstance = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);


            Transform thisBody = thisInstance.transform.Find("Body");
            Vector2 spawnInitVelocity = new Vector2(Random.Range(0f, -55f), Random.Range(-50f, 50f));
            thisBody.gameObject.GetComponent<Rigidbody2D>().AddForce(spawnInitVelocity);
        }

        else if (spawningPrefab.name.Equals("Kunai"))
        {
            spawnPos = new Vector3(transform.position.x, Random.Range(-1.15f, -0.2f), 0f);

            time = 0;
            thisInstance = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);

            Transform thisBody = thisInstance.transform.Find("Body");

        }

        else if (spawningPrefab.name.Equals("Shuriken"))
        {
            //to implement
            spawnPos = new Vector3(transform.position.x, Random.Range(-1.15f, -0.2f), 0f);

            time = 0;
            thisInstance = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);

            Transform thisBody = thisInstance.transform.Find("Body");

        }
        else 
        {
            // default case (not supposed to be reached on version 1)
            spawnPos = new Vector3(transform.position.x, Random.Range(-0.5f, 1f), 0f);
            thisInstance = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);
        }

        thisInstance.name = spawningPrefab.name;
        
        return thisInstance;
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    private IEnumerator increaseSpeedEveryNSeconds(int waitTime) 
    {
        while(Time.timeScale == 1) 
        {
            yield return new WaitForSeconds(waitTime);

            if (maxTime >= 1f)
            {
                maxTime -= 0.1f;
                minTime -= 0.0333f;
            }
            else { break; }
        }

    }


}
