using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    public Item GetItemById(int id)
    {
        Item originalItem = Items.Find(item => item.Id == id);

        if (originalItem == null)
        {
            Debug.LogWarning($"Item with ID {id} not found in the database.");
            return null;
        }

        Item newItem = new Item
        {            
            WeightValue = originalItem.WeightValue,
            ArmorValue = originalItem.ArmorValue,
            HealValue = originalItem.HealValue,
            MaxStack = originalItem.MaxStack,
            Damage = originalItem.Damage,
            Type = originalItem.Type,
            Name = originalItem.Name,
            Id = originalItem.Id,
            Amount = 1,
            Sprite = originalItem.Sprite
        };

        if (newItem.Type == ItemType.Dagger) newItem.Amount = 20;
        if (newItem.Type == ItemType.Arrow) newItem.Amount = 20;

        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public int Amount;
    [Header("Base Attributes")]
    public string Name;
    public int Id;
    public int MaxStack;
    public Sprite Sprite;
    public ItemType Type;
    [Header("if Armor")]    
    public int WeightValue;
    public int ArmorValue;
    [Header("if Potion")]
    public int HealValue;
    [Header("if CanDealDamage")]
    public int Damage;
}
public enum ItemType
{
    Helmet,
    Chestplate,
    Arrow,
    Dagger,
    Potion,
    Weapon,
    Misc
}