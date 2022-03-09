using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateboardingCameraFollow : MonoBehaviour
{
    public DinoSkateboardingPlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.canMove) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - playerMove.normalSpeed * Time.deltaTime);
        }
    }
}
