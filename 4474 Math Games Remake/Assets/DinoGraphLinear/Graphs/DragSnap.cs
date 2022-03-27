using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GraphType type;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPos;
    private bool done;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        originalPos = rectTransform.anchoredPosition;
        done = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (done)
        {
            return;
        }
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (done)
        {
            return;
        }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (done)
        {
            return;
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = originalPos;
    }

    public void Done()
    {
        done = true;
    }
}
