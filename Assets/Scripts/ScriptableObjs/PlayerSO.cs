using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class PlayerSO : SingletonSO<PlayerSO>
{
    [System.Serializable]
    public struct PlayerData
    {
        private int score;
        private int money;
        private string name;
        public int Score { get => score; set => score = value; }
        public int Money { get => money; set => money = value; }
        public string Name { get => name; set => name = value; }
        public ShipInventory[] inventories;
    }

    public PlayerData playerData;

    public delegate void OnValueChangedDelegate();
    public event OnValueChangedDelegate OnValueChanged;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddScore(int s)
    {
        playerData.Score += s;
        OnValueChanged?.Invoke();
    }

    public void AddMoney(int m)
    {
        playerData.Money += m;
        OnValueChanged?.Invoke();
    }

    public void ResetScore()
    {
        playerData.Score = 0;
    }

    public void ResetScoreAndMoney()
    {
        playerData.Score = 0;
        playerData.Money = 0;
    }

    public bool CanUpgrade(EquipmentSO equipment)
    {
        return equipment.GetUpgradeCost() <= playerData.Money;
    }

    public bool Upgrade(EquipmentSO equipment)
    {
        bool canUpgrade = CanUpgrade(equipment);
        if (canUpgrade)
        {
            playerData.Money -= equipment.GetUpgradeCost();
            equipment.Upgrade();
        }
        return canUpgrade;
    }

}
