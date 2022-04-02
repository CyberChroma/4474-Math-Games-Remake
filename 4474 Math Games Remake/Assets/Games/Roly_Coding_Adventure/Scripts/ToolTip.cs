// Credits to CodingMonkey

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private static ToolTip instance;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    [SerializeField] private Camera uiCamera;
    public void Update(){
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.position = transform.parent.TransformPoint(localPoint);
    } // end method


    private void Awake()
    {
        instance = this; // set instance

        backgroundRectTransform = transform.Find("bg").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();

        ShowTooltip("Random text");

        HideTooltip();
    } // end awake

    private void ShowTooltip(string tooltipString){
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 bgSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = bgSize;
    } // end method

    private void HideTooltip(){
        gameObject.SetActive(false);
    } // end method

    public static void ShowTooltip_Static(string tooltipString){
        instance.ShowTooltip(tooltipString);
    } // end method

    public static void HideTooltip_Static(){
        instance.HideTooltip();
    } // end method
} // end method
