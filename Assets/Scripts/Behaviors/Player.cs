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

    public Text m_scoreDisplay;
    public Text m_moneyDisplay;

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
        UpdateStatistics();
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

    void UpdateStatistics()
    {
        m_scoreDisplay.text = "Score: " + Score.ToString();
        m_moneyDisplay.text = "Money: " + Money.ToString();
    }


}
