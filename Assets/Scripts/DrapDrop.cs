// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrapDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 initialPos;

    private void Awake()
    {
        this.name = this.name.Substring(0, 1);
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPos = this.GetComponent<RectTransform>().position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.transform.parent.name == "DragNumbers")
            Instantiate(this, initialPos, new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("DragNumbers").transform);

        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.transform.parent.name == "DragNumbers")
            Destroy(gameObject);
        else if (this.transform.parent.name != "DragNumbers")
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
