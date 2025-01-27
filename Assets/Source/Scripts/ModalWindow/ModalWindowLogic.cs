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
        int checkInt = slot.AddAmountToIndex(-1);
        print (checkInt);
        if (checkInt == 0)
        {
            slot.UpdateAmountText();
            _player.TakeHeal(item.HealValue);
        }
        else if (checkInt < 0)
        {
            slot.DeleteItemInSlot();
        }
    }

    public void BuyNewAmmo(Item item, Slot slot)
    {
        slot.AddAmountToIndex(item.MaxStack - slot.SlotItem.Amount);
    }

}
