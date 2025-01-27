using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> Slots = new List<Slot>();
    public List<EquipSlotLogic> EquipSlots = new List<EquipSlotLogic>();

    public int DaggerAmount;
    public int ArrowAmount;
    public int InventorySlotsAmount;
    public Slot DaggerSlot;
    public Slot ArrowSlot;

    [SerializeField] private DataBase _database;
    [SerializeField] private Slot _slotPrefab;


    [SerializeField] private ArmorInfoWindow _armorInfoWindow;

    private void Start()
    {
        if (_database == null)
        {
            Debug.LogError("Database не назначен в инспекторе.");
            return;
        }

        Slots.Clear();
        for (int i = 0; i < InventorySlotsAmount; i++)
        {
            Slot newSlot = Instantiate(_slotPrefab, this.transform);
            newSlot.gameObject.name = i.ToString();
            newSlot.OnSlotAmmo += FollowAmmoItems;
            newSlot.SlotIndex = i;
            newSlot.SlotItem = null;
            Slots.Add(newSlot);
        }

        Slots[0].AddItemInSlot(_database.GetItemById(1));
        Slots[1].AddItemInSlot(_database.GetItemById(2));
        Slots[2].AddItemInSlot(_database.GetItemById(3));
        Slots[3].AddItemInSlot(_database.GetItemById(4));
        Slots[4].AddItemInSlot(_database.GetItemById(5));
        Slots[5].AddItemInSlot(_database.GetItemById(6));
        Slots[6].AddItemInSlot(_database.GetItemById(7));
    }


    public void SpawnItemInSlot(int itemId)
    {
        for (int i = 0; i < InventorySlotsAmount - 1; i++)
        {
            if (Slots[i].SlotItem == null)
            {
                Slots[i].AddItemInSlot(_database.GetItemById(itemId));
                return;
            }
        }
    }

    private void FollowAmmoItems(ItemType type, int slotIndex)
    {
        if (type == ItemType.Dagger)
        {
            DaggerSlot = Slots[slotIndex];
            DaggerAmount = DaggerSlot.SlotItem.Amount;


        }
        if (type == ItemType.Arrow)
        {
            ArrowSlot = Slots[slotIndex];
            ArrowAmount = ArrowSlot.SlotItem.Amount;
        }
        return;
    }



    public void BuyAmmo(Slot slot)
    {
        slot.AddAmountToIndex(slot.SlotItem.MaxStack - slot.SlotItem.Amount);
        
    }
}
