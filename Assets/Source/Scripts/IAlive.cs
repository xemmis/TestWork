public interface IAlive
{
    public float Health { get; set; }
    public int MaxHealth { get; set; }

    public void TakeHit(float damage, int accuransy);
    public void TakeHeal(float healAmount);
    public void Die();
}