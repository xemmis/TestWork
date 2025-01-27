using UnityEngine;

public class Player : MonoBehaviour, IAlive
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }

    [SerializeField] private EquipSlotLogic _equipSlot;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
    }

    public void TakeHit(int damage)
    {
        _animator.SetTrigger("Hurt");
        Health =- damage - _equipSlot.EquipedChestplate.ArmorValue - _equipSlot.EquipedHelmet.ArmorValue;
        if (Health <= 0) Die();
    }

    public void TakeHeal(int healAmount)
    {
        if (healAmount < 0) return;

        if (healAmount + Health > MaxHealth)
        {
            Health = MaxHealth;
            return;
        }
        Health += healAmount;
    }


    public void Die()
    {
        _animator.SetTrigger("Death");
        print("умер");
    }
}
