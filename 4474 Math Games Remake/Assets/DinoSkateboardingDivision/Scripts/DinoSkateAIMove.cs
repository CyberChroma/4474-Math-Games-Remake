using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateAIMove : MonoBehaviour
{
    public float normalSpeed;
    public float randomFailTimeMin;
    public float randomFailTimeMax;
    public float flopDuration;

    public float stepMoveSideSmoothing;
    public float stepMoveBackSmoothing;
    public float stepMoveMaxSpeed;

    public float moveToEndSmoothing;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool kickflipping;
    [HideInInspector] public bool doneRace;
    [HideInInspector] public bool raceOver;
    [HideInInspector] public Transform raceDonePos;
    [HideInInspector] public Animator anim;

    private float startX;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        StartCoroutine(RandomFailing());
    }

    // Update is called once per frame
    void Update()
    {
        if (raceOver) {
            transform.position = Vector3.Lerp(transform.position, raceDonePos.position, moveToEndSmoothing * Time.deltaTime);
        }
        else if (canMove) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - normalSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!raceOver) {
            if (other.CompareTag("StepStop")) {
                Kickflip();
            }
            else if (other.CompareTag("Win")) {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - normalSpeed * Time.deltaTime * 10);
                StopAllCoroutines();
                doneRace = true;
                canMove = false;
            }
        }
    }

    void Kickflip()
    {
        StartCoroutine(MoveForStep());
    }

    IEnumerator MoveForStep()
    {
        yield return new WaitForSeconds(1.1f);
        anim.SetTrigger("Kickflip");
        kickflipping = true;
        while (Mathf.Abs(transform.position.x - (startX - 0.1f)) >= 0.5f) {
            float newX = Mathf.Lerp(transform.position.x, startX - 0.1f, stepMoveSideSmoothing * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;
        }
        yield return null;
        kickflipping = false;
        while (Mathf.Abs(transform.position.x - startX) >= 0.01f) {
            float newX = Mathf.Lerp(transform.position.x, startX, stepMoveSideSmoothing * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - newX) > stepMoveMaxSpeed * Time.deltaTime) {
                newX = transform.position.x - stepMoveMaxSpeed * Time.deltaTime;
            }
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    IEnumerator RandomFailing()
    {
        while(true) {
            yield return new WaitForSeconds(Random.Range(randomFailTimeMin, randomFailTimeMax));
            if (canMove && !kickflipping) {
                canMove = false;
                yield return new WaitForSeconds(flopDuration);
                canMove = true;
            }
        }
    }
}
