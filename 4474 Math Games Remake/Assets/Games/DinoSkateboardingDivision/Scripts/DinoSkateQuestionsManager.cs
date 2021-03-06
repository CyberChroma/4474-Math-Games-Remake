using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoSkateQuestionsManager : MonoBehaviour
{
    public float questionMoveSpeed;
    public TMP_Text questionText;
    public Transform question;
    public DinoSkateAnswers[] questionAnswers;
    public Transform questionOnScreenPos;
    public Transform questionOffScreenPos;

    public AudioClip equationVoiceLine;

    private bool questionActive;
    private string[] questions;
    private int questionNum = -1;
    private DinoSkateVoiceManager voiceManager;

    // Start is called before the first frame update
    void Start()
    {
        questions = new string[questionAnswers.Length];
        int[] answers = new int[3];
        for (int i = 0; i < questionAnswers.Length; i++) {
            //Make question
            int divisor = Random.Range(1, 11);
            int quotient = Random.Range(0, 11);
            int dividend = divisor * quotient;
            if (i == 9) {
                questions[i] = string.Format("Q{0}: {1} ? {2}", i + 1, dividend, divisor);
            }
            else {
                questions[i] = string.Format("Q{0}:  {1} ? {2}", i + 1, dividend, divisor);
            }
            int randomAnswer1 = Random.Range(0, 11);
            int randomAnswer2 = Random.Range(0, 11);
            while(randomAnswer1 == quotient) {
                randomAnswer1 = Random.Range(0, 11);
            }
            while (randomAnswer2 == quotient || randomAnswer2 == randomAnswer1) {
                randomAnswer2 = Random.Range(0, 11);
            }
            int correctSpot = Random.Range(0, 3);
            answers = new int[3];
            answers[correctSpot] = quotient;
            switch (correctSpot) {
                case 0:
                    answers[1] = randomAnswer1;
                    answers[2] = randomAnswer2;
                    break;
                case 1:
                    answers[0] = randomAnswer1;
                    answers[2] = randomAnswer2;
                    break;
                case 2:
                    answers[0] = randomAnswer1;
                    answers[1] = randomAnswer2;
                    break;
            }

            questionAnswers[i].SetupNumbers(answers, correctSpot);
        }
        voiceManager = FindObjectOfType<DinoSkateVoiceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (questionActive) {
            question.position = Vector3.MoveTowards(question.position, questionOnScreenPos.position, questionMoveSpeed * Time.deltaTime);
        } else {
            question.position = Vector3.MoveTowards(question.position, questionOffScreenPos.position, questionMoveSpeed * Time.deltaTime);
        }
    }

    public void ActivateQuestion()
    {
        voiceManager.PlayVoiceLine(equationVoiceLine);
        questionNum++;
        questionActive = true;
        questionText.text = questions[questionNum];
        StartCoroutine(WaitToRepeatVoiceLine());
    }

    IEnumerator WaitToRepeatVoiceLine()
    {
        while (true) {
            yield return new WaitForSeconds(8);
            voiceManager.PlayVoiceLine(equationVoiceLine);
        }
    }
    
    public void Solved()
    {
        questionActive = false;
    }

    public void Wrong()
    {
        StopAllCoroutines();
        StartCoroutine(WaitToRepeatVoiceLine());
    }
}
