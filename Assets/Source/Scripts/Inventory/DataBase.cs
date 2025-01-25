using System.Collections.Generic;
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
            Damage = originalItem.Damage,

            Name = originalItem.Name,
            Id = originalItem.Id,
            Amount = 1,
            Sprite = originalItem.Sprite
        };

        return newItem;
    }
}

[System.Serializable]
public class Item
{
    [HideInInspector] public int Amount;
    [Header("Base Attributes")]
    public string Name;
    public int Id;    
    public Sprite Sprite;
    public ItemType Type;
    [Header("if Armor")]
    public bool Helmet;
    public int WeightValue;
    public int ArmorValue;
    [Header("if Potion")]
    public int HealValue;
    [Header("if CanDealDamage")]
    public int Damage;
}
public enum ItemType
{
    Armor,
    Ammo,
    Potion,
    Weapon,
    Misc
}