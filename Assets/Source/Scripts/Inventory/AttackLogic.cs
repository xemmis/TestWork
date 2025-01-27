using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private Player _player;
    public PlayerChoice Choice;
    public enum PlayerChoice
    {
        Dagger,
        Bow
    }

    public void ChangeChoice(bool isDagger)
    {
        if (isDagger) Choice = PlayerChoice.Dagger; else Choice = PlayerChoice.Bow;
    }

    public void AttackEnemy()
    {
        if (Choice == PlayerChoice.Dagger) ThrowDagger();
        else UseABow();
    }

    public void ThrowDagger()
    {
        if (_inventory.DaggerAmount >= 3)
        {
            _inventory.DaggerAmount -= 3;
            _inventory.DaggerSlot.AddAmountToIndex(-3);
            _player.DealDamage(_inventory.DaggerSlot.SlotItem.Damage);
        }
        else print("DontHaveEnoughDagger");
    }

    public void UseABow()
    {
        if (_inventory.ArrowAmount >= 1) 
        {
            _inventory.ArrowAmount--;
            _inventory.ArrowSlot.AddAmountToIndex(-1);
            _player.DealDamage(_inventory.ArrowSlot.SlotItem.Damage);
        }
        else print("DontHaveEnoughArrow");
    }
}
