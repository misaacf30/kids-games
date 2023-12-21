// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            GameObject dragObject = eventData.pointerDrag;
            if (this.transform.childCount == 0)
            {
                dragObject.GetComponent<RectTransform>().transform.parent = this.transform;
                dragObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                Debug.Log("drop if");
            }       
            else
            {
                GameObject child = this.gameObject.transform.GetChild(0).gameObject;
                if(child != dragObject)
                    Destroy(child);
                dragObject.GetComponent<RectTransform>().transform.parent = this.transform;
                dragObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                Debug.Log("drop else");
            }         
        }
    }
}