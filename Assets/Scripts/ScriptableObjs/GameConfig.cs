using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu]
public class GameConfig : SingletonSO<GameConfig>
{
    [System.Serializable]
    private struct GameSave
    {
        public int score;
        public int money;
        public int[][] equipmentLevels;
        //public int gameVersion;
    }

    public bool debugMode = false;
    public bool resetOnStart = false;
    public int defaultEquipmentLevel = 1;

    //public PlayerSO.PlayerData pData;

    private string savePath;

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Config/Game Config")]
    public static void ShowGameSettings()
    {
        UnityEditor.Selection.activeObject = Instance;
    }
#endif


    private void OnEnable()
    {
        if (resetOnStart)
        {
            ResetGame();
        }
        savePath = Application.persistentDataPath + "/save.dat";
    }

    public void ResetGame()
    {
        PlayerSO player = PlayerSO.Instance;
        for (int i = 0; i < player.playerData.inventories.Length; i++)
        {
            for (int j = 0; j < player.playerData.inventories[i].equipments.Length; j++)
            {
                player.playerData.inventories[i].equipments[j].SetLevel(defaultEquipmentLevel);
            }
        }
        player.ResetScoreAndMoney();
    }

    public void SaveGame ()
    {
        //Wrap Data
        GameSave save = new GameSave();
        PlayerSO.PlayerData pData = PlayerSO.Instance.playerData;
        save.score = pData.Score;
        save.money = pData.Money;
        save.equipmentLevels = new int[pData.inventories.Length][];
        for (int i = 0; i < pData.inventories.Length; i++)
        {
            save.equipmentLevels[i] = new int[pData.inventories[i].equipments.Length];
            for (int j = 0; j < pData.inventories[i].equipments.Length; j++)
            {
                save.equipmentLevels[i][j] = pData.inventories[i].equipments[j].Level;
            }
        }
        //Save
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            //Load
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            GameSave save = (GameSave)bf.Deserialize(file);
            file.Close();
            //Unwrap Data
            PlayerSO p = PlayerSO.Instance;
            p.playerData.Score = save.score;
            p.playerData.Money = save.money;
            for (int i = 0; i < save.equipmentLevels.Length; i++)
            {
                for (int j = 0; j < save.equipmentLevels[i].Length; j++)
                {
                    PlayerSO.Instance.playerData.inventories[i].equipments[j].SetLevel(save.equipmentLevels[i][j]);
                }
            }
        }
    }

}
