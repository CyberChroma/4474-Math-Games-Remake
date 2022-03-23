using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragDropManager : MonoBehaviour
{

    public GameObject holding;

    public GameObject [] slots;

    public Color slotColor, slotHighlight;

    public Button runBtn;

    public bool nextCommand, canContinue = true;

    public GameObject appleReminder;

    private AudioSource audioSource;
    public AudioClip signalFX;


    public void Start(){
        //slots = GameObject.FindGameObjectsWithTag("slot");
        slotColor = slots[0].GetComponent<Image>().color;

    } // end start

    public void Awake(){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = signalFX;
    } // end method
    public void RunCoroutine(){
        StartCoroutine("Run");
    } // end methode

    IEnumerator Run(){
        runBtn.interactable = false;
        
        canContinue = true;
        for(int i = slots.Length-1; i >= 0; i--){
            if(slots[i].GetComponent<JigsawDrop>().holding != null && canContinue){
                slots[i].GetComponent<JigsawDrop>().holding.GetComponent<Roly_Command>().CommandRoly();
                audioSource.Play();
              //  yield return new WaitWhile(() => nextCommand == false);
                yield return new WaitForSeconds(2.5f);
                if(GameObject.Find("Player") != null)
                    GameObject.Find("Player").GetComponent<Roly_Movement>().checkCollision = true;
                yield return new WaitForSeconds(0.2f);
                if(GameObject.Find("Player") != null)
                    GameObject.Find("Player").GetComponent<Roly_Movement>().checkCollision = false;
              //  Debug.Log("End Command");

            } // end if
        
        } // end for
        GameObject.Find("Player").transform.position = GameObject.Find("Player").GetComponent<Roly_Movement>().startPos.position;
        GameObject.Find("Player").GetComponent<Roly_Movement>().moveDirection = new Vector2(-1, -1);
        GameObject.Find("Player").GetComponent<Roly_Movement>().changeSprite();

        if(!GameObject.Find("Player").GetComponent<Roly_Movement>().wormReminder.activeSelf && !GameObject.Find("Player").GetComponent<Roly_Movement>().fallReminder.activeSelf)
            appleReminder.SetActive(true);

        canContinue = true;
        runBtn.interactable = true;
    } // end method

    public void HighlightOpenSlots(){
        for(int i = 0; i < slots.Length; i++){
            if(slots[i].GetComponent<JigsawDrop>().holding == null || slots[i].GetComponent<JigsawDrop>().holding == holding)
                slots[i].GetComponent<Image>().color = slotHighlight;
        } // end for
    } // end method

    public void DimOpenSlots(){
        for(int i = 0; i < slots.Length; i++){
            slots[i].GetComponent<Image>().color = slotColor;
        } // end for
    } // end method
} // end class

