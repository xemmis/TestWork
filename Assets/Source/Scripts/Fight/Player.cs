using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IAlive
{
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    
    [SerializeField] private EquipSlotLogic _equipSlot;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _healthBar;

    public static Action OnPlayerDeath;

    private void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        ChangeFillAmount();
    }

    private void ChangeFillAmount()
    {
        _healthBar.fillAmount = Health / 100;
    }

    public void TakeHit(float damage, int accuransy)
    {
        if (accuransy == 1) Health -= (damage - _equipSlot.EquipedHelmet.ArmorValue);
        if (accuransy == 2) Health -= (damage - _equipSlot.EquipedHelmet.ArmorValue);

        ChangeFillAmount();
        _animator.SetTrigger("Hurt");
        
        if (Health <= 0) Die();
        
    }
    
    public void SetHealth(float health)
    {
        Health = health;
        ChangeFillAmount();
    }

    public void DealDamage() { }
    public void DealDamage(float damage)
    {
        int RandomAccuransy = UnityEngine.Random.Range(1, 3);
        _enemy.TakeHit(damage, RandomAccuransy);
        _animator.SetTrigger("Attack");
    }

    public void TakeHeal(float healAmount)
    {
        if (healAmount < 0) return;

        if (healAmount + Health > MaxHealth)
        {
            Health = MaxHealth;
            ChangeFillAmount();
            return;
        }
        ChangeFillAmount();
        Health += healAmount;
    }


    public void Die()
    {
        _animator.SetTrigger("Death");
        OnPlayerDeath?.Invoke();
    }
}
