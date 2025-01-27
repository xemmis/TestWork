using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipSlotLogic : MonoBehaviour
{

    [SerializeField] private Image _helmetImage;
    [SerializeField] private Image _chestplateImage;
    [SerializeField] private Slot _selectedSlot;
    [SerializeField] private Item _selectedItem;
    [SerializeField] private ArmorInfoWindow _armorInfoWindow;

    public Slot EquipedHelmetSlot;
    public Slot EquipedChestplateSlot;

    public Item EquipedChestplate;
    public Item EquipedHelmet;

    private void Start()
    {
        EquipedHelmet = null;
        EquipedChestplate = null;
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
            if (EquipedHelmet != null) EquipedHelmet.IsEquiped = false;

            EquipedHelmet = _selectedItem;
            EquipedHelmetSlot = _selectedSlot;
            _helmetImage.sprite = _selectedItem.Sprite;
            _helmetImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            if (EquipedChestplate != null) EquipedChestplate.IsEquiped = false;
            
            EquipedChestplate = _selectedItem;
            EquipedChestplateSlot = _selectedSlot;
            _chestplateImage.sprite = _selectedItem.Sprite;
            _chestplateImage.color = new Color(1, 1, 1, 1);
        }
        slot.SlotItem.IsEquiped = true;
    }

    public void DeEquipArmor()
    {
        Slot deleteSlot = _armorInfoWindow.SelectedSlot;

        if (deleteSlot != EquipedChestplateSlot && deleteSlot != EquipedHelmetSlot && !deleteSlot.SlotItem.IsEquiped)
        {
            deleteSlot.DeleteItemInSlot();
            return;
        }

        else
        {
            if (deleteSlot.SlotItem.Type == ItemType.Helmet)
            {
                _helmetImage.sprite = null;
                _helmetImage.color = new Color(1, 1, 1, 0);
                deleteSlot.DeleteItemInSlot();
                EquipedHelmet = null;
                EquipedHelmetSlot = null;
            }
            else
            {
                _chestplateImage.sprite = null;
                _chestplateImage.color = new Color(1, 1, 1, 0);
                deleteSlot.DeleteItemInSlot();
                EquipedChestplate = null;
                EquipedChestplateSlot = null;
            }
        }
    }
}