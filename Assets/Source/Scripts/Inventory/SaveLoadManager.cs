using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Item> SavedSlotsItems;

    public Slot EquipedChestplateSlot;
    public Slot EquipedHelmetSlot;

    public Item EquipedChestplate;
    public Item EquipedHelmet;

    public float PlayerHealth;
    public float EnemyHealth;
}

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private string _fileName;

    [SerializeField] private List<Item> _inventorySlotsItem;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] EquipSlotLogic _equipSlotLogic;

    public void SaveGame()
    {
        print("trySave");
        GameData gameData = new GameData
        {
            SavedSlotsItems = new List<Item>(),
            PlayerHealth = _player.Health,
            EnemyHealth = _enemy.Health,
            EquipedChestplate = _equipSlotLogic.EquipedChestplate,
            EquipedHelmet = _equipSlotLogic.EquipedHelmet,
            EquipedChestplateSlot = _equipSlotLogic.EquipedChestplateSlot,
            EquipedHelmetSlot = _equipSlotLogic.EquipedHelmetSlot,
        };

        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            gameData.SavedSlotsItems.Add(_inventory.Slots[i].SlotItem);
        }

        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/" + _fileName, json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/" + _fileName;
        if (File.Exists(path))
        {
            print("tryload");
            string json = File.ReadAllText(path);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            for (int i = 0; i < _inventory.Slots.Count; i++)
            {
                _inventory.Slots[i].AddItemInSlot(gameData.SavedSlotsItems[i]);
            }
            if (gameData.EquipedChestplate != null && gameData.EquipedChestplateSlot != null)
                _equipSlotLogic.EquipArmor(gameData.EquipedChestplate, gameData.EquipedChestplateSlot);
            if (gameData.EquipedHelmet != null && gameData.EquipedHelmetSlot != null)
                _equipSlotLogic.EquipArmor(gameData.EquipedHelmet, gameData.EquipedHelmetSlot);
            _player.SetHealth(gameData.PlayerHealth);
            _enemy.SetHealth(gameData.EnemyHealth);
        }
    }
}