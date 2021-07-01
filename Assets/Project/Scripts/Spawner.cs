using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject sphere;
    public GameObject square;
    public int spheresSetCount;
    public Transform[] spawnPoints;
    int spheresSpawned;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (spheresSpawned <= spheresSetCount)
        {
            GameObject sphereTarget = Instantiate(sphere, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            sphereTarget.transform.localPosition = Vector3.zero;
            spheresSpawned += 1;
        }
    }
}
