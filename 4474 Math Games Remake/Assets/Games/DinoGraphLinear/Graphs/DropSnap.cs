using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSnap : MonoBehaviour, IDropHandler
{
    public GraphType type;
    public ScoreManger scoreManger;
    private RectTransform myRectTransform;
    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        scoreManger = GetComponentInParent<ScoreManger>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            DragSnap dragSnap = eventData.pointerDrag.GetComponent<DragSnap>();
            if (rectTransform != null && dragSnap != null && dragSnap.type == type)
            {
                rectTransform.anchoredPosition = myRectTransform.anchoredPosition;
                scoreManger.UpdateScore();
                dragSnap.Done();
            } else
            {
                scoreManger.Wrong();
            }
        }
    }
}
