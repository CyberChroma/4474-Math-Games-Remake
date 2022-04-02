using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinoSkateAnswers : MonoBehaviour
{
    public float wrongAnswerStunTime;

    public TMP_Text[] answerTexts;

    public string chooseAnswerDialogue;
    public string wrongAnswerDialogue;
    public string rightAnswerDialogue;
    public Color chooseAnswerDefaultColor;
    public Color wrongAnswerColor;
    public Color rightAnswerColor;
    public TMP_Text chooseAnswerText;

    public AudioClip answerWrongVoiceLine;
    public AudioClip answerCorrectVoiceLine;

    private int correctAnswerNum;
    private DinoSkateQuestionsManager questionsManager;
    private DinoSkatePlayerMove playerMove;
    private Button[] answerButtons;
    private DinoSkateVoiceManager voiceManager;

    void Start()
    {
        questionsManager = FindObjectOfType<DinoSkateQuestionsManager>();
        playerMove = FindObjectOfType<DinoSkatePlayerMove>();
        answerButtons = GetComponentsInChildren<Button>();
        voiceManager = FindObjectOfType<DinoSkateVoiceManager>();
        chooseAnswerText.text = chooseAnswerDialogue;
        chooseAnswerText.color = chooseAnswerDefaultColor;
    }

    public void SetupNumbers(int[] newNumbers, int correctNum)
    {
        for (int i = 0; i < answerTexts.Length; i++) {
            answerTexts[i].text = newNumbers[i].ToString();
        }
        correctAnswerNum = correctNum;
    }

    public void SelectAnswer(int answerNum)
    {
        if (answerNum == correctAnswerNum) {
            voiceManager.PlayVoiceLine(answerCorrectVoiceLine);
            answerTexts[answerNum].color = Color.green;
            playerMove.Kickflip(answerNum);
            playerMove.anim.ResetTrigger("OffBoard");
            for (int i = 0; i < answerButtons.Length; i++) {
                answerButtons[i].interactable = false;
            }
            StopAllCoroutines();
            StartCoroutine(WaitToBringTextDown());
        }
        else {
            voiceManager.PlayVoiceLine(answerWrongVoiceLine);
            answerTexts[answerNum].color = Color.red;
            questionsManager.Wrong();
            playerMove.Flop();
            StopAllCoroutines();
            StartCoroutine(StopAnswers());
            chooseAnswerText.text = wrongAnswerDialogue;
            chooseAnswerText.color = wrongAnswerColor;
        }
    }

    IEnumerator WaitToBringTextDown()
    {
        chooseAnswerText.text = rightAnswerDialogue;
        chooseAnswerText.color = rightAnswerColor;
        questionsManager.StopAllCoroutines();
        yield return new WaitForSeconds(1);
        questionsManager.Solved();
        yield return new WaitForSeconds(1);
        chooseAnswerText.text = chooseAnswerDialogue;
        chooseAnswerText.color = chooseAnswerDefaultColor;
    }

    IEnumerator StopAnswers()
    {
        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].interactable = false;
        }
        yield return new WaitForSeconds(wrongAnswerStunTime);
        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].interactable = true;
        }
        yield return new WaitForSeconds(1);
        chooseAnswerText.text = chooseAnswerDialogue;
        chooseAnswerText.color = chooseAnswerDefaultColor;
    }
}