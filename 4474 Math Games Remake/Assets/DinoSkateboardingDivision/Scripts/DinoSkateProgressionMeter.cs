using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoSkateProgressionMeter : MonoBehaviour
{
    public float scale;
    public float offset;
    public float offsetX;
    public RectTransform progressionIcon;
    public Sprite ripper;
    public Sprite slash;
    public Sprite skitch;
    public Sprite burley;

    // Update is called once per frame
    void Update()
    {
        progressionIcon.anchoredPosition = new Vector3(offsetX, transform.position.z * scale + offset, 0);
    }

    public void SetupIcon(int characterSelected)
    {
        switch(characterSelected) {
            case 0:
                progressionIcon.GetComponent<Image>().sprite = ripper;
                break;
            case 1:
                progressionIcon.GetComponent<Image>().sprite = slash;
                break;
            case 2:
                progressionIcon.GetComponent<Image>().sprite = skitch;
                break;
            case 3:
                progressionIcon.GetComponent<Image>().sprite = burley;
                break;
        }
    }
}
