using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateEnvironmentSpawner : MonoBehaviour
{
    public float trackDistance;
    public float leftRightVariance;
    public float spawnDistanceMin;
    public float spawnDistanceMax;

    public Transform environmentParent;

    public GameObject[] environmentalObject;

    // Start is called before the first frame update
    void Start()
    {
        float curDistance = 0;
        while(curDistance > trackDistance) {
            curDistance -= Random.Range(spawnDistanceMin, spawnDistanceMax);
            GameObject objectToSpawn = environmentalObject[Random.Range(0, environmentalObject.Length)];
            Transform newObject = Instantiate(objectToSpawn, new Vector3(-3 + Random.Range(-leftRightVariance, leftRightVariance), 0.5f, curDistance), Quaternion.identity, environmentParent).transform;
            newObject.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }

        curDistance = 0;
        while (curDistance > trackDistance) {
            curDistance -= Random.Range(spawnDistanceMin, spawnDistanceMax);
            GameObject objectToSpawn = environmentalObject[Random.Range(0, environmentalObject.Length)];
            Transform newObject = Instantiate(objectToSpawn, new Vector3(3 + Random.Range(-leftRightVariance, leftRightVariance), 0.5f, curDistance), Quaternion.identity, environmentParent).transform;
            newObject.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }
}