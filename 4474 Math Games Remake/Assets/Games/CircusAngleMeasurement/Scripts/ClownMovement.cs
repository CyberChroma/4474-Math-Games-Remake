using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMovement : MonoBehaviour
{
    public float speed;
    public Sprite greenTarget;

    public Rigidbody rb;
    private TargetSpawner targetSpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    public void StartMoving ()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target")) {
            rb.velocity = Vector3.zero;
            transform.parent = other.transform;
            targetSpawner.ClearTargets();
        }
    }
}
