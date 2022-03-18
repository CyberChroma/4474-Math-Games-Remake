using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinoSkateAnswers : MonoBehaviour
{
    public TMP_Text[] answerTexts;

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
            questionsManager.Solved();
            playerMove.Kickflip(answerNum);
            for(int i = 0; i < answerButtons.Length; i++) {
                answerButtons[i].interactable = false;
            }
        }
        else {
            voiceManager.PlayVoiceLine(answerWrongVoiceLine);
            answerTexts[answerNum].color = Color.red;
            questionsManager.Wrong();
            answerButtons[answerNum].interactable = false;
            playerMove.Flop();
        }
    }
}
