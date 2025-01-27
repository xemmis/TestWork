using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    private Inventory _inventory;
    [SerializeField] private Image _icon;    
    public TextMeshProUGUI CountText;
    public Item SlotItem = null;
    public int SlotIndex;

    private Button _button;

    public Action <ItemType,int> OnSlotAmmo;
    public static Action<Item, Slot> OnItemClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
        _inventory = GetComponentInParent<Inventory>();
    }

    private void Start()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(ItemClicked);
        }
        ChangeSlotAmountText();
    }

    public void ItemClicked()
    {
        if (SlotItem == null || SlotItem.Id == 0) return;
        OnItemClicked?.Invoke(SlotItem, this);
    }

    public void ChangeSlotAmountText()
    {
        if (SlotItem == null)
        {
            CountText.text = "";
            return;
        }

        if (SlotItem.Amount > 1) CountText.text = SlotItem.Amount.ToString();
        else CountText.text = "";
    }

    public void UpdateSlot(Item item, int amount)
    {
        if (item != null)
        {
            if (item.Type == ItemType.Dagger || item.Type == ItemType.Arrow)
            {
                OnSlotAmmo?.Invoke(item.Type, SlotIndex);
                print("ammo");
            }

            SlotItem = item;
            SlotItem.Amount = amount;
            _icon.sprite = SlotItem.Sprite;
            _icon.color = new Color(1, 1, 1, 1);
            ChangeSlotAmountText();
        }
        else
        {
            SlotItem = null;
            _icon.color = new Color(1, 1, 1, 0);
            ChangeSlotAmountText();
        }
    }

    public void UpdateSlot(Item item)
    {
        if (item != null)
        {
            if (item.Type == ItemType.Dagger || item.Type == ItemType.Arrow)
            {
                OnSlotAmmo?.Invoke(item.Type, SlotIndex);
                print("ammo");
            }

            SlotItem = item;
            SlotItem.Amount = item.Amount;
            _icon.sprite = SlotItem.Sprite;
            _icon.color = new Color(1, 1, 1, 1);
            ChangeSlotAmountText();
        }
        else
        {
            SlotItem = null;
            _icon.color = new Color(1, 1, 1, 0);
            ChangeSlotAmountText();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            DraggableItem draggedItem = draggedObject.GetComponent<DraggableItem>();
            if (draggedItem.OriginalSlot.gameObject == this.gameObject)
            {
                return;
            }

            if (draggedItem != null && draggedItem.OriginalSlot.SlotItem != null)
            {
                Item tempItem = SlotItem;
                if (tempItem != null && tempItem.Id == draggedItem.OriginalSlot.SlotItem.Id)
                {
                    if (tempItem.Amount + draggedItem.OriginalSlot.SlotItem.Amount > tempItem.MaxStack)
                    {
                        int remains = tempItem.Amount + draggedItem.OriginalSlot.SlotItem.Amount - tempItem.MaxStack;
                        print(remains);
                        tempItem.Amount = tempItem.MaxStack;
                        UpdateSlot(tempItem);
                        _inventory.AddItemInSlot(0, draggedItem.OriginalSlot.SlotItem.Id, remains);
                        draggedItem.OriginalSlot.UpdateSlot(null);
                        _inventory.CheckAmmoAmount();
                    }
                    else
                    {
                        tempItem.Amount += draggedItem.OriginalSlot.SlotItem.Amount;
                        UpdateSlot(tempItem);
                        draggedItem.OriginalSlot.UpdateSlot(null);
                        _inventory.CheckAmmoAmount();
                    }

                }
                else
                {
                    UpdateSlot(draggedItem.OriginalSlot.SlotItem);
                    if (tempItem != null && tempItem.Id != 0) draggedItem.OriginalSlot.UpdateSlot(tempItem);
                    else draggedItem.OriginalSlot.UpdateSlot(null);
                    _inventory.CheckAmmoAmount();
                }
            }
        }
    }
}
