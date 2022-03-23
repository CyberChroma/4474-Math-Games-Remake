// Credits go to CodingMonkey, modified to check if dragging for the dropper, along with changing size on mouse over

using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private DragDropManager manager;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private AudioSource audioSource;
    public AudioClip pickUpFX, dropFX;
    public bool dragging = false;

    public void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    } // end awake

    public void OnBeginDrag(PointerEventData eventData){
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        dragging = true;
    } // end method

    public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    } // end method

    public void OnEndDrag(PointerEventData eventData){
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        dragging = false;
    } // end method

    public void OnPointerDown(PointerEventData eventData){
        manager.holding = this.gameObject;
        audioSource.clip = pickUpFX;
        audioSource.Play();
        manager.HighlightOpenSlots();

    } // end method

    public void OnPointerUp(PointerEventData eventData){
        manager.DimOpenSlots();
        audioSource.clip = dropFX;
        audioSource.Play();
       // manager.holding = null;
    } // end method

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("hovering");
        rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
    } // end method

    public void OnPointerExit(PointerEventData eventData){
        rectTransform.localScale = new Vector3(1, 1, 1);
    } // end method
} // end class
