using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Index;
    public Sprite Image;
}
