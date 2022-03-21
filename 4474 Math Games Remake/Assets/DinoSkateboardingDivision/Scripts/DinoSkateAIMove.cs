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
    [HideInInspector] public DinoSkateDinoBlink dinoBlink;

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
                dinoBlink.overrideBlink = false;
                dinoBlink.eyelids[2].SetActive(false);
                dinoBlink.eyelids[3].SetActive(false);
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
        kickflipping = true;
        yield return new WaitForSeconds(1.1f);
        anim.SetTrigger("Kickflip");
        while (Mathf.Abs(transform.position.x - (startX - 0.1f)) >= 0.5f) {
            float newX = Mathf.Lerp(transform.position.x, startX - 0.1f, stepMoveSideSmoothing * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;
        }
        yield return null;
        while (Mathf.Abs(transform.position.x - startX) >= 0.02f) {
            float newX = Mathf.Lerp(transform.position.x, startX, stepMoveSideSmoothing * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - newX) > stepMoveMaxSpeed * Time.deltaTime) {
                newX = transform.position.x - stepMoveMaxSpeed * Time.deltaTime;
            }
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;
        }
        kickflipping = false;
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    IEnumerator RandomFailing()
    {
        while(true) {
            yield return new WaitForSeconds(Random.Range(randomFailTimeMin, randomFailTimeMax));
            if (canMove && !kickflipping) {
                canMove = false;
                anim.SetTrigger("Flop");
                StartCoroutine(ActivateLowerEyelids());
                yield return new WaitForSeconds(flopDuration);
                canMove = true;
            }
        }
    }

    IEnumerator ActivateLowerEyelids()
    {
        dinoBlink.overrideBlink = true;
        dinoBlink.eyelids[0].SetActive(true);
        dinoBlink.eyelids[1].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dinoBlink.overrideBlink = false;
        dinoBlink.eyelids[0].SetActive(false);
        dinoBlink.eyelids[1].SetActive(false);
    }
}
