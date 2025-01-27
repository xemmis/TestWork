using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private Player _player;


    private bool _canThrowDagger;
    private bool _canUseBow;
    private int _daggerSlotInt = 0;
    private int _arrowSlotInt = 0;
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

    private void CheckSlotAmount(List<Slot> slots, ItemType type)
    {
        if (type == ItemType.Dagger)
        {
            for (int i = 0; i < _inventory.DaggerSlot.Count; i++)
            {
                if (_inventory.DaggerSlot[i].SlotItem.Amount >= 3)
                {
                    _daggerSlotInt = i;
                    _canThrowDagger = true;
                    return;
                }                
            }
            _canThrowDagger = false;
        }
        if (type == ItemType.Arrow)
        {
            for (int i = 0; i < _inventory.ArrowSlot.Count; i++)
            {
                if (_inventory.ArrowSlot[i].SlotItem.Amount > 0)
                {
                    _arrowSlotInt = i;
                    _canUseBow = true;
                    return;
                }
            }
            _canUseBow = false;
        }
    }

    public void ThrowDagger()
    {
        CheckSlotAmount(_inventory.DaggerSlot, ItemType.Dagger);
        if (!_canThrowDagger) return;

        int amountCheck = _inventory.DaggerSlot[_daggerSlotInt].AddAmountToIndex(-3);
        if (amountCheck < 0)
        {
            _inventory.DaggerSlot[_daggerSlotInt].DeleteItemInSlot();
            return;
        }
        _player.DealDamage(_inventory.DaggerSlot[_daggerSlotInt].SlotItem.Damage);


    }

    public void UseABow()
    {
        CheckSlotAmount(_inventory.ArrowSlot, ItemType.Arrow);
        if (!_canUseBow) return;

        int amountCheck = _inventory.ArrowSlot[_arrowSlotInt].AddAmountToIndex(-1);
        if (amountCheck < 0)
        {
            _inventory.ArrowSlot[_arrowSlotInt].DeleteItemInSlot();
            return;
        }
        _player.DealDamage(_inventory.ArrowSlot[_arrowSlotInt].SlotItem.Damage);

    }
}
