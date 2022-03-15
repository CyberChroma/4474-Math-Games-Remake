using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinoSkateboardingAnswers : MonoBehaviour
{
    public TMP_Text[] answerTexts;

    private int correctAnswerNum;
    private DinoSkateboardingQuestionsManager questionsManager;
    private DinoSkateboardingPlayerMove playerMove;
    private Button[] answerButtons;

    void Start()
    {
        questionsManager = FindObjectOfType<DinoSkateboardingQuestionsManager>();
        playerMove = FindObjectOfType<DinoSkateboardingPlayerMove>();
        answerButtons = GetComponentsInChildren<Button>();
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
            // Color text green
            questionsManager.Solved();
            playerMove.Kickflip(answerNum);
            for(int i = 0; i < answerButtons.Length; i++) {
                answerButtons[i].interactable = false;
            }
            print("Correct!");
        }
        else {
            // Color text red
            playerMove.Flop();
            print("Wrong!");
        }
    }
}
