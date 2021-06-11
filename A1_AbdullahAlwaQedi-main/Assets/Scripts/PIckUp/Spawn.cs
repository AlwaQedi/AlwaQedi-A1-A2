using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        int Randoms = Random.Range(0, 4);
        Instantiate(spawnedObject[Randoms], transform.position, transform.rotation);
       /* if (Randoms == 1)
        {
            Instantiate(spawnedObject[0], transform.position, transform.rotation);
        }
        else if (Randoms == 2)
        {
            Instantiate(spawnedObject[1], transform.position, transform.rotation);
        }
        else
        {
            Instantiate(spawnedObject[2], transform.position, transform.rotation);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}