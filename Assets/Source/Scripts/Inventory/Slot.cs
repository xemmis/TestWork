using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image _icon;
    public TextMeshProUGUI CountText;
    public Item SlotItem = null;
    public int SlotIndex;

    private Button _button;

    public static Action<Item, Slot> OnItemClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(ItemClicked);
        }
    }

    public void ItemClicked()
    {
        if (SlotItem == null || SlotItem.Id == 0) return;
        OnItemClicked?.Invoke(SlotItem, this);
    }

    public void UpdateSlot(Item item)
    {
        if (item != null)
        {
            SlotItem = item;
            SlotItem.Amount = item.Amount;
            _icon.sprite = SlotItem.Sprite;
            _icon.color = new Color(1, 1, 1, 1);
            CountText.text = SlotItem.Amount > 1 ? SlotItem.Amount.ToString() : "";
        }
        else
        {
            print("Null");
            SlotItem = null;
            _icon.color = new Color(1, 1, 1, 0);
            CountText.text = "";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            DraggableItem draggedItem = draggedObject.GetComponent<DraggableItem>();
            if (draggedItem != null && draggedItem.OriginalSlot.SlotItem != null)
            {
                Item tempItem = SlotItem;
                if (tempItem != null && tempItem.Id == draggedItem.OriginalSlot.SlotItem.Id)
                {
                    tempItem.Amount += draggedItem.OriginalSlot.SlotItem.Amount;
                    UpdateSlot(tempItem);
                    draggedItem.OriginalSlot.UpdateSlot(null);
                }
                else
                {
                    UpdateSlot(draggedItem.OriginalSlot.SlotItem);
                    if (tempItem != null && tempItem.Id != 0) draggedItem.OriginalSlot.UpdateSlot(tempItem);
                    else draggedItem.OriginalSlot.UpdateSlot(null);
                }
            }
        }
    }
}
