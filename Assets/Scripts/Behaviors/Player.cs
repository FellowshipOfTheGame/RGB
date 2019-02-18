using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;

    public int Score { get; private set; } = 0;
    public int Money { get; private set; } = 0;

    public ShipInventory[] inventories;

    public delegate void OnValueChangedDelegate();
    public event OnValueChangedDelegate OnValueChanged;

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

    private void Update()
    {
        
    }

    public void AddScore(int s)
    {
        Score += s;
        OnValueChanged?.Invoke();
    }

    public void AddMoney(int m)
    {
        Money += m;
        OnValueChanged?.Invoke();
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
