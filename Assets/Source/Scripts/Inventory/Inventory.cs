using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> Slots = new List<Slot>();
    public List<EquipSlotLogic> EquipSlots = new List<EquipSlotLogic>();
    public int DaggerAmount;
    public int ArrowAmount;

    [SerializeField] private int _inventorySlotsAmount;
    [SerializeField] private DataBase _database;
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private Slot _daggerSlot;
    [SerializeField] private Slot _arrowSlot;
    [SerializeField] private ArmorInfoWindow _armorInfoWindow;

    private void Start()
    {
        if (_database == null)
        {
            Debug.LogError("Database не назначен в инспекторе.");
            return;
        }

        Slots.Clear();
        for (int i = 0; i < _inventorySlotsAmount; i++)
        {
            Slot newSlot = Instantiate(_slotPrefab, this.transform);
            newSlot.OnSlotAmmo += CheckAmmoAmount;
            newSlot.SlotIndex = i;
            newSlot.SlotItem = null;
            Slots.Add(newSlot);
        }


        AddItemInSlot(0, 1, 1);
        AddItemInSlot(0, 2, 1);
        AddItemInSlot(0, 3, 1);
        AddItemInSlot(0, 4, 1);
        AddItemInSlot(0, 6, 100);
        AddItemInSlot(0, 7, 15);
        AddItemInSlot(0, 5, 50);
    }

    public void BuyAmmo(ItemType ammoType)
    {
        if (ammoType == ItemType.Dagger)
        {
            _daggerSlot.SlotItem.Amount = _daggerSlot.SlotItem.MaxStack;
        }
        if (ammoType == ItemType.Arrow)
        {
            _arrowSlot.SlotItem.Amount = _arrowSlot.SlotItem.MaxStack;
        }
        CheckAmmoAmount();
    }

    public void CheckAmmoAmount(ItemType type,int slotIndex)
    {        
        if (type == ItemType.Dagger)
        {
            _daggerSlot = Slots[slotIndex];
            DaggerAmount = Slots[slotIndex].SlotItem.Amount;
        }
        if (type == ItemType.Arrow)
        {
            _arrowSlot = Slots[slotIndex];
            ArrowAmount = Slots[slotIndex].SlotItem.Amount;
        }
    }

    public void CheckAmmoAmount()
    {
        DaggerAmount = _daggerSlot.SlotItem.Amount;
        ArrowAmount = _arrowSlot.SlotItem.Amount;
        _daggerSlot.CountText.text = _daggerSlot.SlotItem.Amount.ToString();
        _arrowSlot.CountText.text = _arrowSlot.SlotItem.Amount.ToString();
    }

    public void AddItemInSlot(int slotIndex, int itemId, int amount)
    {
        if (_database.GetItemById(itemId) == null) return;
        Item newItem = _database.GetItemById(itemId);
        if (newItem.MaxStack < amount)
        {
            newItem.Amount = newItem.MaxStack;
            AddItemInSlot(slotIndex++, itemId, amount - newItem.MaxStack);

        }
        else newItem.Amount = amount;


        if (Slots[slotIndex].SlotItem != null && Slots[slotIndex].SlotItem.Id != itemId)
        {
            AddItemInSlot(slotIndex + 1, itemId, newItem.Amount);
        }

        else if (Slots[slotIndex].SlotItem != null && Slots[slotIndex].SlotItem.Id == itemId)
        {
            if (Slots[slotIndex].SlotItem.Amount + newItem.Amount > Slots[slotIndex].SlotItem.MaxStack)
            {
                Slots[slotIndex].SlotItem.Amount = Slots[slotIndex].SlotItem.MaxStack;
                AddItemInSlot(slotIndex + 1, itemId, newItem.Amount + Slots[slotIndex].SlotItem.MaxStack - Slots[slotIndex].SlotItem.Amount);
            }
            else
            {
                Slots[slotIndex].SlotItem.Amount += newItem.Amount;
            }
        }

        else
        {
            Slots[slotIndex].UpdateSlot(newItem);
        }

    }

    public void AddItemInSlot(int itemId)
    {
        if (_database.GetItemById(itemId) == null) return;
        Item newItem = _database.GetItemById(itemId);
        for (int i = 0; i < _inventorySlotsAmount; i++)
        {
            if (Slots[i].SlotItem == null)
            {
                Slots[i].UpdateSlot(newItem);
                return;
            }
        }
    }
}
