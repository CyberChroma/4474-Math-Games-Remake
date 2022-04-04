using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetSpawner : MonoBehaviour
{
    public int targetsToSpawn = 4;
    public int[] angles = new int[4];

    public GameObject targetPrefab;

    private int[] possible10Angles = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180 };
    private int[] possible15Angles = new int[] { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165, 180 };

    public Animator[] targetAnims;

    public TextMeshProUGUI anglesText;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTargets(possible10Angles);
    }

    void SpawnTargets(int[] possibleAngles)
    {
        List<int> allAngles = new List<int>(possibleAngles);
        targetAnims = new Animator[targetsToSpawn];
        for (int i = 0; i < angles.Length; i++) {
            int newAngleIndex = Random.Range(0, allAngles.Count);
            angles[i] = allAngles[newAngleIndex];
            GameObject newTarget = Instantiate(targetPrefab, transform);
            newTarget.transform.Rotate(Vector3.forward * angles[i]);
            targetAnims[i] = newTarget.GetComponent<Animator>();
            allAngles.RemoveAt(newAngleIndex);
        }
        anglesText.text = angles[0].ToString() + "°";
    }

    public void NextWave()
    {
        StartCoroutine(ClearWave());
    }

    IEnumerator ClearWave()
    {
        yield return new WaitForSeconds(2.75f);
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        if (Random.Range(0, 2) == 0) {
            SpawnTargets(possible10Angles);
        }
        else {
            SpawnTargets(possible15Angles);
        }
    }

    public void ClearTargets()
    {
        for (int i = 0; i < targetAnims.Length; i++) {
            targetAnims[i].SetTrigger("Go Up");
        }
    }
}
