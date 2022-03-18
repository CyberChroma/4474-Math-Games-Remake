using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateDinoBlink : MonoBehaviour
{
    public float blinkTimeMin;
    public float blinkTimeMax;
    public float blinkDuration;

    public GameObject[] eyelids;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < eyelids.Length; i++) {
            eyelids[i].SetActive(false);
        }
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true) {
            yield return new WaitForSeconds(Random.Range(blinkTimeMin, blinkTimeMax));
            for (int i = 0; i < eyelids.Length; i++) {
                eyelids[i].SetActive(true);
            }
            yield return new WaitForSeconds(blinkDuration);
            for (int i = 0; i < eyelids.Length; i++) {
                eyelids[i].SetActive(false);
            }
        }
    }
}
