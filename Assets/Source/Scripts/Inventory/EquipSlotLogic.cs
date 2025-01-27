using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipSlotLogic : MonoBehaviour
{

    [SerializeField] private Image _helmetImage;
    [SerializeField] private Image _chestplateImage;
    [SerializeField] private Slot _selectedSlot;
    [SerializeField] private Slot _equipedHelmetSlot;
    [SerializeField] private Slot _equipedChestplateSlot;
    [SerializeField] private Item _selectedItem;
    [SerializeField] private ArmorInfoWindow _armorInfoWindow;

    public Item EquipedChestplate;
    public Item EquipedHelmet;

    private void Start()
    {
        _armorInfoWindow.OnEquipAction += EquipArmor;
        _chestplateImage.color = new Color(1, 1, 1, 0);
        _helmetImage.color = new Color(1, 1, 1, 0);
    }


    public void EquipArmor(Item item, Slot slot)
    {
        _selectedSlot = slot;
        _selectedItem = item;

        if (_selectedItem.Type == ItemType.Helmet)
        {
            EquipedHelmet = _selectedItem;
            _equipedHelmetSlot = _selectedSlot;
            _helmetImage.sprite = _selectedItem.Sprite;
            _helmetImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            EquipedChestplate = _selectedItem;
            _equipedChestplateSlot = _selectedSlot;
            _chestplateImage.sprite = _selectedItem.Sprite;
            _chestplateImage.color = new Color(1, 1, 1, 1);
        }
    }

    public void DeEquipArmor()
    {
        Slot deleteSlot = _armorInfoWindow.SelectedSlot;

        if (deleteSlot != _equipedChestplateSlot && deleteSlot != _equipedHelmetSlot)
        {
            deleteSlot.UpdateSlot(null);
            return;
        }

        else
        {
            if (deleteSlot.SlotItem.Type == ItemType.Helmet)
            {
                _helmetImage.sprite = null;
                _helmetImage.color = new Color(1, 1, 1, 0);
                deleteSlot.UpdateSlot(null);
                EquipedHelmet = null;
                _equipedHelmetSlot = null;
            }
            else
            {
                _chestplateImage.sprite = null;
                _chestplateImage.color = new Color(1, 1, 1, 0);
                deleteSlot.UpdateSlot(null);
                EquipedChestplate = null;
                _equipedChestplateSlot = null;
            }
        }
    }
}