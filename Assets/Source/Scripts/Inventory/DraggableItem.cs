using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Slot OriginalSlot;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        OriginalSlot = GetComponentInParent<Slot>();
        if (OriginalSlot == null)
        {
            Debug.LogError("DraggableItem не найден в дочернем объекте Slot.");
            return;
        }

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OriginalSlot.SlotItem == null || OriginalSlot.SlotItem.Id == 0) return;

        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_canvas.transform); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OriginalSlot.SlotItem == null || OriginalSlot.SlotItem.Id == 0) return;

        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(OriginalSlot.transform);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}
