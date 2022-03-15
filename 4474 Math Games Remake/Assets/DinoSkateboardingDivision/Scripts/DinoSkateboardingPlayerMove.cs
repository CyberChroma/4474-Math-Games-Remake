using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateboardingPlayerMove : MonoBehaviour
{
    public float normalSpeed;

    public float stepSideDis;
    public float stepMoveSideSmoothing;
    public float stepMoveBackSmoothing;
    public float stepMoveMaxSpeed;

    public float moveToEndSmoothing;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool doneRace;
    public Transform raceDonePos;

    private Animator anim;
    private DinoSkateboardingQuestionsManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        questionManager = FindObjectOfType<DinoSkateboardingQuestionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doneRace) {
            transform.position = Vector3.Lerp(transform.position, raceDonePos.position, moveToEndSmoothing * Time.deltaTime);
        }
        else if (canMove) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - normalSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StepStop")) {
            questionManager.ActivateQuestion();
            StartCoroutine(waitToStop());
        } else if (other.CompareTag("Win")) {
            canMove = false;
            doneRace = true;
        }
    }

    IEnumerator waitToStop()
    {
        yield return new WaitForSeconds(1);
        canMove = false;
    }

    public void Kickflip(int answerNum)
    {
        canMove = true;
        anim.SetTrigger("Kickflip");
        StopAllCoroutines();
        StartCoroutine(MoveForStep(answerNum));
    }

    IEnumerator MoveForStep(int answerNum)
    {
        switch (answerNum) {
            case 0:
                while (Mathf.Abs(transform.position.x + stepSideDis) >= 1f) {
                    float newX = Mathf.Lerp(transform.position.x, -stepSideDis, stepMoveSideSmoothing * Time.deltaTime);
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                yield return null;
                while (Mathf.Abs(transform.position.x) >= 0.01f) {
                    float newX = Mathf.Lerp(transform.position.x, 0, stepMoveSideSmoothing * Time.deltaTime);
                    if (Mathf.Abs(transform.position.x - newX) > stepMoveMaxSpeed * Time.deltaTime) {
                        newX = transform.position.x + stepMoveMaxSpeed * Time.deltaTime;
                    }
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                break;
            case 1:
                while (Mathf.Abs(transform.position.x - (stepSideDis/3)) >= 0.5f) {
                    float newX = Mathf.Lerp(transform.position.x, stepSideDis/3, stepMoveSideSmoothing * Time.deltaTime);
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                yield return null;
                while (Mathf.Abs(transform.position.x) >= 0.01f) {
                    float newX = Mathf.Lerp(transform.position.x, 0, stepMoveSideSmoothing * Time.deltaTime);
                    if (Mathf.Abs(transform.position.x - newX) > stepMoveMaxSpeed * Time.deltaTime) {
                        newX = transform.position.x - stepMoveMaxSpeed * Time.deltaTime;
                    }
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                break;
            case 2:
                while (Mathf.Abs(transform.position.x - stepSideDis) >= 1f) {
                    float newX = Mathf.Lerp(transform.position.x, stepSideDis, stepMoveSideSmoothing * Time.deltaTime);
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                yield return null;
                while (Mathf.Abs(transform.position.x) >= 0.01f) {
                    float newX = Mathf.Lerp(transform.position.x, 0, stepMoveSideSmoothing * Time.deltaTime);
                    if (Mathf.Abs(transform.position.x - newX) > stepMoveMaxSpeed * Time.deltaTime) {
                        newX = transform.position.x - stepMoveMaxSpeed * Time.deltaTime;
                    }
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                    yield return null;
                }
                break;
        }
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    public void Flop()
    {
        // Do animation stuff
        // anim.SetTrigger("Flop");
    }
}
