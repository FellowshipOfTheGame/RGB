using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;

    public int Score { get; private set; } = 0;
    public int Money { get; private set; } = 0;

    public ShipInventory[] inventories;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
    }

    public void AddScore(int s)
    {
        Score += s;
    }

    public void AddMoney(int m)
    {
        Money += m;
    }

    public bool CanUpgrade (EquipmentSO equipment)
    {
        return equipment.GetUpgradeCost() <= Money;
    }

    public bool Upgrade (EquipmentSO equipment)
    {
        bool canUpgrade = CanUpgrade(equipment);
        equipment.Upgrade();
        return canUpgrade;
    }

}
