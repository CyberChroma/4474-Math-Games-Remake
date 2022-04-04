using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurn : MonoBehaviour
{
    public bool followingMouse;
    public Transform cannon;
    public Sprite greenTarget;

    private int curTargetNum;
    private TargetSpawner targetSpawner;
    private ClownMovement clownMovement;
    private Vector3 clownStartingPos;
    private bool canTurn;

    // Start is called before the first frame update
    void Start()
    {
        targetSpawner = FindObjectOfType<TargetSpawner>();
        transform.right = Vector3.up;
        clownMovement = GetComponentInChildren<ClownMovement>();
        clownStartingPos = clownMovement.transform.localPosition;
        canTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTurn) {
            if (followingMouse) {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                if (worldPosition.y < 0.1f) {
                    worldPosition.y = 0.1f;
                }
                transform.right = worldPosition;
            }
            if (Input.GetMouseButtonUp(0)) {
                RotateToNearestTarget();
            }
        }
    }

    private void RotateToNearestTarget()
    {
        followingMouse = false;
        float cannonAngle = transform.rotation.eulerAngles.z;
        if (transform.rotation.eulerAngles.y > 179) {
            cannonAngle = 180;
            transform.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        int minDisIndex = -1;
        float minDis = 1000;

        for (int i = 0; i < targetSpawner.angles.Length; i++) {
            if (Mathf.Abs(targetSpawner.angles[i] - cannonAngle) < minDis) {
                minDis = Mathf.Abs(targetSpawner.angles[i] - cannonAngle);
                minDisIndex = i;
            }
        }
        curTargetNum = minDisIndex;

        transform.rotation = Quaternion.Euler(Vector3.forward * targetSpawner.angles[minDisIndex]);
    }

    private void OnMouseDown()
    {
        if (canTurn) {
            followingMouse = true;
        }
    }

    public void Fire()
    {
        if (canTurn) {
            clownMovement.StartMoving();
            if (curTargetNum == 0) {
                targetSpawner.NextWave();
                StartCoroutine(WaitToTurnGreen());
            }
            StartCoroutine(ResetClown());
        }
    }

    IEnumerator WaitToTurnGreen()
    {
        yield return new WaitForSeconds(0.4f);
        targetSpawner.targetAnims[0].GetComponentInChildren<SpriteRenderer>().sprite = greenTarget;
    }

    IEnumerator ResetClown()
    {
        canTurn = false;
        yield return new WaitForSeconds(2.6f);
        clownMovement.rb.velocity = Vector3.zero;
        clownMovement.transform.parent = cannon;
        clownMovement.transform.localPosition = clownStartingPos;
        clownMovement.transform.localRotation = Quaternion.identity;
        canTurn = true;
    }
}
