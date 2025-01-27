using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IAlive
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private Image _healthBar;

    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int Armor { get; set; }

    public static Action OnEnemyDeath;

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

    public void DealDamage()
    {
        int RandomAccuransy = UnityEngine.Random.Range(1, 3);
        _animator.SetTrigger("Attack");
        _player.TakeHit(15, RandomAccuransy);
    }

    private IEnumerator DealDamageTick()
    {
        yield return new WaitForSeconds(.5f);
        DealDamage();
    }
    public void SetHealth(float health)
    {
        Health = health;
        ChangeFillAmount();
    }
    public void Die()
    {
        _animator.SetTrigger("Death");
        OnEnemyDeath?.Invoke();
    }

    public void Recover()
    {
        _animator.SetTrigger("Recover");
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

    public void TakeHit(float damage, int accuransy)
    {
        Health -= damage;
        ChangeFillAmount();
        _animator.SetTrigger("Hurt");

        if (Health <= 0) Die();

        StartCoroutine(DealDamageTick());
    }
}
