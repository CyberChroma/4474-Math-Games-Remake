// Credits go to CodingMonkey, modified to store held object

using UnityEngine;
using UnityEngine.EventSystems;
public class JigsawDrop : MonoBehaviour, IDropHandler
{   

    public GameObject dragdropManager;
    public GameObject holding; // the gameobject being held

    private AudioSource audioSource;
    public AudioClip insertedFX;

    public void Awake(){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = insertedFX;
    } // end method
    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            audioSource.Play();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            holding = dragdropManager.GetComponent<DragDropManager>().holding; // store held object
            dragdropManager.GetComponent<DragDropManager>().holding = null; // set to null after
        } // end if
    } // end method

    void Update(){
        if (holding != null && holding.GetComponent<DragDrop>().dragging == true)
            holding = null;
    } // end method

} // end class
