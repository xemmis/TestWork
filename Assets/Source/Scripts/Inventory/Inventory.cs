using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> Slots = new List<Slot>();
    public List<EquipSlot> EquipSlots = new List<EquipSlot>(); // Уточните назначение или удалите

    [SerializeField] private DataBase _database;
    [SerializeField] private Slot _slotPrefab;


    private void Start()
    {
        // Проверка на наличие базы данных
        if (_database == null)
        {
            Debug.LogError("Database не назначен в инспекторе.");
            return;
        }

        // Создаём слоты и добавляем их в список
        Slots.Clear();
        for (int i = 0; i < 30; i++) // Укажите необходимое количество слотов
        {
            Slot newSlot = Instantiate(_slotPrefab, this.transform);
            newSlot.SlotIndex = i;
            Slots.Add(newSlot);
        }

        // Инициализация примеров предметов
        Slots[0].UpdateSlot(_database.GetItemById(1));
        Slots[1].UpdateSlot(_database.GetItemById(2));
        Slots[2].UpdateSlot(_database.GetItemById(2));
        Slots[3].UpdateSlot(_database.GetItemById(2));
        Slots[4].UpdateSlot(_database.GetItemById(1));
    }
}
