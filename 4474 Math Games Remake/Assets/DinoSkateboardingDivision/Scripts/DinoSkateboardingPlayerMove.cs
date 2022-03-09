using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateboardingPlayerMove : MonoBehaviour
{
    public float normalSpeed;
    public bool canMove;

    private DinoSkateboardingQuestionsManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        questionManager = FindObjectOfType<DinoSkateboardingQuestionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - normalSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            canMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StepStop")) {
            questionManager.ActivateQuestion();
            StartCoroutine(waitToStop());
        }
    }

    IEnumerator waitToStop()
    {
        yield return new WaitForSeconds(1);
        canMove = false;
    }

    public void Kickflip()
    {
        canMove = true;
        // Do animation stuff
    }

    public void Flop()
    {
        // Do animation stuff
    }
}
