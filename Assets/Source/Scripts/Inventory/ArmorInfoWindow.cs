using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArmorInfoWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armorValue;
    [SerializeField] private TextMeshProUGUI _weightValue;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private Image _modalImage;
    [SerializeField] private GameObject _modalWindow;
    [SerializeField] private Inventory _inventory;
    public Slot SelectedSlot;
    private Item _selectedItem;

    public Action<Item,Slot> OnBuyAction;
    public Action<Item, Slot> OnEquipAction;
    public Action<Item, Slot> OnHealAction;

    public InteractWay Way;

    public enum InteractWay
    {
        Buy,
        Heal,
        Equip
    }

    private void Start()
    {
        Slot.OnItemClicked += OpenWindow;
    }

    private void OpenWindow(Item item, Slot slot)
    {
        switch (item.Type)
        {
            case ItemType.Helmet:
            case ItemType.Chestplate:
                _interactText.text = "Экипировать";
                Way = InteractWay.Equip;
                break;
            case ItemType.Arrow:
            case ItemType.Dagger:
                _interactText.text = "Купить";
                Way = InteractWay.Buy;
                break;
            case ItemType.Potion:
                _interactText.text = "Лечить";
                Way = InteractWay.Heal;
                break;

        }
        SelectedSlot = slot;
        _selectedItem = item;
        _armorValue.text = item.ArmorValue.ToString();
        _weightValue.text = item.WeightValue.ToString();
        _modalImage.sprite = item.Sprite;
        _modalWindow.SetActive(true);
    }

    public void Interact()
    {
        switch (Way)
        {
            case InteractWay.Equip:
                OnEquipAction?.Invoke(_selectedItem,SelectedSlot);
                break;
            case InteractWay.Buy:
                OnBuyAction?.Invoke(_selectedItem, SelectedSlot);
                _inventory.BuyAmmo(SelectedSlot);
                break;
            case InteractWay.Heal:
                OnHealAction?.Invoke(_selectedItem, SelectedSlot);
                break;
        }
    }
}

public class PlayerInput : MonoBehaviour
{
    public PlayerChoice Choice;

    public enum PlayerChoice
    {
        Dagger,
        Bow
    }

    public void AttackEnemy()
    {

    }
}