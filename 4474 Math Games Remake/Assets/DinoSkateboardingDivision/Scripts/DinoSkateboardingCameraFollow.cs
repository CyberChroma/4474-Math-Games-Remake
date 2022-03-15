using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateboardingCameraFollow : MonoBehaviour
{
    public float moveToEndSmoothing;
    public DinoSkateboardingPlayerMove playerMove;
    public Transform raceDonePos;

    // Update is called once per frame
    void Update()
    {
        if (playerMove.doneRace) {
            transform.position = Vector3.Lerp(transform.position, raceDonePos.position, moveToEndSmoothing * Time.deltaTime);
        }
        else if (playerMove.canMove) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - playerMove.normalSpeed * Time.deltaTime);
        }
    }
}
