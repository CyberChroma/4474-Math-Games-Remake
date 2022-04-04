using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntController : MonoBehaviour
{
    private SpriteRenderer handleSpriteRenderer;
    private CannonTurn cannonTurn;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        handleSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        cannonTurn = FindObjectOfType<CannonTurn>();
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        handleSpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }

    private void OnMouseUp()
    {
        handleSpriteRenderer.color = new Color(1, 1, 1);
        anim.SetTrigger("Fire");
        cannonTurn.Fire();
    }

    private void OnMouseEnter()
    {
        handleSpriteRenderer.color = new Color(0.75f, 0.75f, 0.75f);
    }

    private void OnMouseExit()
    {
        handleSpriteRenderer.color = new Color(1, 1, 1);
    }
}
