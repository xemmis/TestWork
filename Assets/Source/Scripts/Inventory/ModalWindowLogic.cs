using UnityEngine;

public class ModalWindowLogic : MonoBehaviour
{
    [SerializeField] private ArmorInfoWindow _armorInfoWindow;
    [SerializeField] private Player _player;

    private void Start()
    {
        _armorInfoWindow.OnHealAction += UseHealPotion;
        _armorInfoWindow.OnBuyAction += BuyNewAmmo;
    }

    public void UseHealPotion(Item item, Slot slot)
    {
        slot.AddAmountToIndex(-1);
        _player.TakeHeal(item.HealValue);
    }

    public void BuyNewAmmo(Item item, Slot slot)
    {
        slot.AddAmountToIndex(item.MaxStack - slot.SlotItem.Amount);
    }

}
