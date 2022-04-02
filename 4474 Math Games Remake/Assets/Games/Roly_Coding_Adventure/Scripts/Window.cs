using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public string text;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData){
        ToolTip.ShowTooltip_Static(text);
    } // end method
    public void OnPointerExit(PointerEventData eventData){
        ToolTip.HideTooltip_Static();
    } // end method
  
} // end method
