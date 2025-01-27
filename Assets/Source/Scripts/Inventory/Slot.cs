using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image _icon;
    private Button _button;

    public TextMeshProUGUI CountText;
    public Item SlotItem = null;
    public int SlotIndex;

    public Action<ItemType, int> OnSlotAmmo;
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

    public void AddItemInSlot(Item item)
    {
        if (item  == null || item.Id == 0)
        {
            DeleteItemInSlot();
            return;
        }
        SlotItem = item;
        if (item.Type == ItemType.Arrow || item.Type == ItemType.Dagger) OnSlotAmmo?.Invoke(item.Type, SlotIndex);
        UpdateAmountText();

        _icon.sprite = item.Sprite;
        _icon.color = new Color(1, 1, 1, 1);
    }

    public int AddAmountToIndex(int amount)
    {       
        if (SlotItem.Amount + amount <= 0)
        {
            return -1;
        }

        int newAmount = SlotItem.Amount + amount;

        if (newAmount > SlotItem.MaxStack)
        {
            UpdateAmountText();
            return newAmount - SlotItem.MaxStack; 
        }

        SlotItem.Amount = newAmount; 
        UpdateAmountText();
        return 0; 
    }

    public void UpdateView()
    {
        if (SlotItem != null && SlotItem.Id > 0)
        {
            if (SlotItem.Type == ItemType.Arrow || SlotItem.Type == ItemType.Dagger) OnSlotAmmo?.Invoke(SlotItem.Type, SlotIndex);

            _icon.sprite = SlotItem.Sprite;
            _icon.color = new Color(1, 1, 1, 1);
            UpdateAmountText();
        }
        else
        {
            UpdateAmountText();
            SlotItem = null;
            _icon.color = new Color(1, 1, 1, 0);
        }

    }

    public void OnButtonClick()
    {
        OnItemClicked?.Invoke(SlotItem, this);
    }

    public void UpdateAmountText()
    {
        if (SlotItem.Amount > 1) CountText.text = SlotItem.Amount.ToString();
        else CountText.text = "";
    }

    public void DeleteItemInSlot()
    {
        SlotItem = null;
        _icon.sprite = null;
        _icon.color = new Color(1, 1, 1, 0);
        CountText.text = null;
    }

    private void DragDropAddLogic(Slot anotherItem)
    {
        if (SlotItem != null)
        {
            if (SlotItem.Id > 0)
            {
                if (anotherItem.SlotItem.Id == SlotItem.Id)
                {
                    int missAmount = anotherItem.AddAmountToIndex(SlotItem.Amount);

                    if (missAmount > 0)
                    {
                        SlotItem.Amount = SlotItem.MaxStack;
                        anotherItem.SlotItem.Amount = missAmount;

                        UpdateAmountText();
                        anotherItem.UpdateAmountText();
                        return;
                    }
                    AddAmountToIndex(SlotItem.Amount);
                    anotherItem.DeleteItemInSlot();
                }
                else
                {
                    Item tempCurrentSlotItem = SlotItem;
                    Item tempAnotherSlotItem = anotherItem.SlotItem;
                    anotherItem.AddItemInSlot(tempCurrentSlotItem);
                    AddItemInSlot(tempAnotherSlotItem);
                }
            }
        }
        else
        {
            AddItemInSlot(anotherItem.SlotItem);
            anotherItem.DeleteItemInSlot();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            DraggableItem draggableItem = draggedObject.GetComponent<DraggableItem>();
            Slot draggableSlot = draggableItem.OriginalSlot;
            if (draggableItem == null || draggableSlot == null) return;

            if (draggableSlot.SlotItem != null)
            {
                DragDropAddLogic(draggableSlot);
            }
            UpdateAmountText();
        }
    }
}
