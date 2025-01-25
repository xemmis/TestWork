using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> Slots = new List<Slot>();
    public List<EquipSlot> EquipSlots = new List<EquipSlot>(); // �������� ���������� ��� �������

    [SerializeField] private DataBase _database;
    [SerializeField] private Slot _slotPrefab;


    private void Start()
    {
        // �������� �� ������� ���� ������
        if (_database == null)
        {
            Debug.LogError("Database �� �������� � ����������.");
            return;
        }

        // ������ ����� � ��������� �� � ������
        Slots.Clear();
        for (int i = 0; i < 30; i++) // ������� ����������� ���������� ������
        {
            Slot newSlot = Instantiate(_slotPrefab, this.transform);
            newSlot.SlotIndex = i;
            Slots.Add(newSlot);
        }

        // ������������� �������� ���������
        Slots[0].UpdateSlot(_database.GetItemById(1));
        Slots[1].UpdateSlot(_database.GetItemById(2));
        Slots[2].UpdateSlot(_database.GetItemById(2));
        Slots[3].UpdateSlot(_database.GetItemById(2));
        Slots[4].UpdateSlot(_database.GetItemById(1));
    }
}
