using UnityEngine;

public class RandomEnemyLoot : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void Start()
    {
        Enemy.OnEnemyDeath += SpawnRandLoot;
    }

    public void SpawnRandLoot()
    {
        _inventory.SpawnItemInSlot(UnityEngine.Random.Range(1, 7));
    }

}