using UnityEngine;

public class Enemy : MonoBehaviour, IAlive
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int Armor { get; set; }

    private void Start()
    {
        MaxHealth = 100;
    }

    public void DealDamage(int damage)
    {
        _animator.SetTrigger("Attack");
        _player.TakeHit(damage);
    }

    public void Die()
    {
        _animator.SetTrigger("Death");
        
    }

    public void Recover()
    {
        _animator.SetTrigger("Recover");
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

    public void TakeHit(int damage)
    {
        _animator.SetTrigger("Hurt");
        DealDamage(Random.Range(5,15));
    }
}


public interface IAlive
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public void TakeHit(int damage);
    public void TakeHeal(int healAmount);
    public void Die();
}