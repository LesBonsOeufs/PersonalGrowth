using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DraggableRectTransform : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform rectTransform;
    private Vector2 mousePosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 lDeltaDrag = eventData.position - mousePosition;
        rectTransform.anchoredPosition += lDeltaDrag;
        mousePosition = eventData.position;
    }
}