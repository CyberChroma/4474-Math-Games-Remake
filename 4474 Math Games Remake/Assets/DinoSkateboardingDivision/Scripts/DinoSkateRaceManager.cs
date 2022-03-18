using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoSkateRaceManager : MonoBehaviour
{
    public DinoSkatePlayerMove playerMove;
    public DinoSkateAIMove AIMove1;
    public DinoSkateAIMove AIMove2;
    public TMP_Text placeText;

    public Transform endPos1st;
    public Transform endPos2nd;
    public Transform endPos3rd;

    public Color goldColor;
    public Color silverColor;
    public Color bronzeColor;

    public GameObject readyText;
    public GameObject setText;
    public GameObject goText;

    private bool raceOver;
    private int playerPlace;

    // Start is called before the first frame update
    void Start()
    {
        readyText.SetActive(false);
        setText.SetActive(false);
        goText.SetActive(false);
    }

    public void StartRace() {
        StartCoroutine(ReadySetGo());
    }

    IEnumerator ReadySetGo()
    {
        yield return new WaitForSeconds(0.5f);
        //Ready
        readyText.SetActive(true);
        yield return new WaitForSeconds(1);
        readyText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //Set
        setText.SetActive(true);
        yield return new WaitForSeconds(1);
        setText.SetActive(false);
        yield return new WaitForSeconds(1f);
        //Go!
        goText.SetActive(true);
        playerMove.canMove = true;
        AIMove1.canMove = true;
        AIMove2.canMove = true;
        yield return new WaitForSeconds(2);
        goText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!raceOver) {
            CalcPlayerPlace();
            UpdatePlaceText();
            AIFinishRace();
            EndRace();
        }
    }

    void CalcPlayerPlace()
    {
        playerPlace = 3;
        if (playerMove.transform.position.z < AIMove1.transform.position.z) {
            playerPlace--;
        }
        if (playerMove.transform.position.z < AIMove2.transform.position.z) {
            playerPlace--;
        }
    }

    void UpdatePlaceText()
    {
        switch (playerPlace) {
            case 1:
                placeText.text = "1<sup>st</sub>";
                placeText.color = goldColor;
                break;
            case 2:
                placeText.text = "2<sup>nd</sub>";
                placeText.color = silverColor;

                break;
            case 3:
                placeText.text = "3<sup>rd</sub>";
                placeText.color = bronzeColor;
                break;
        }
    }

    void AIFinishRace()
    {
        if (AIMove1.doneRace && AIMove1.raceDonePos == null) {
            if (AIMove2.doneRace) {
                AIMove1.raceDonePos = endPos2nd;
            }
            else {
                AIMove1.raceDonePos = endPos1st;
            }
        }

        if (AIMove2.doneRace && AIMove2.raceDonePos == null) {
            if (AIMove1.doneRace) {
                AIMove2.raceDonePos = endPos2nd;
            }
            else {
                AIMove2.raceDonePos = endPos1st;
            }
        }
    }

    void EndRace()
    {
        if (playerMove.doneRace) {
            raceOver = true;
            switch (playerPlace) {
                case 1:
                    playerMove.raceDonePos = endPos1st;
                    break;
                case 2:
                    playerMove.raceDonePos = endPos2nd;
                    break;
                case 3:
                    playerMove.raceDonePos = endPos3rd;
                    break;
            }

            AIMove1.StopAllCoroutines();
            AIMove1.canMove = false;
            AIMove1.raceOver = true;
            if (AIMove1.raceDonePos == null) {
                int AI1Place = 3;
                if (AIMove1.transform.position.z < playerMove.transform.position.z) {
                    AI1Place--;
                }
                if (AIMove1.transform.position.z < AIMove2.transform.position.z) {
                    AI1Place--;
                }
                switch (AI1Place) {
                    case 1:
                        AIMove1.raceDonePos = endPos1st;
                        break;
                    case 2:
                        AIMove1.raceDonePos = endPos2nd;
                        break;
                    case 3:
                        AIMove1.raceDonePos = endPos3rd;
                        break;
                }
            }

            AIMove2.StopAllCoroutines();
            AIMove2.canMove = false;
            AIMove2.raceOver = true;
            if (AIMove2.raceDonePos == null) {
                int AI2Place = 3;
                if (AIMove2.transform.position.z < playerMove.transform.position.z) {
                    AI2Place--;
                }
                if (AIMove2.transform.position.z < AIMove1.transform.position.z) {
                    AI2Place--;
                }
                switch (AI2Place) {
                    case 1:
                        AIMove2.raceDonePos = endPos1st;
                        break;
                    case 2:
                        AIMove2.raceDonePos = endPos2nd;
                        break;
                    case 3:
                        AIMove2.raceDonePos = endPos3rd;
                        break;
                }
            }

            playerMove.GetComponent<DinoSkateProgressionMeter>().enabled = false;
            AIMove1.GetComponent<DinoSkateProgressionMeter>().enabled = false;
            AIMove2.GetComponent<DinoSkateProgressionMeter>().enabled = false;
        }
    }
}
