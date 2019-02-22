using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles equipment upgrades UI
/// </summary>
public class UpgradeScreen : MonoBehaviour
{
    public UIScroller shipScroller;
    public UIScroller[] equipmentScrollers; // Equipment scroller for each ship

    private AsyncOperation asyncLoad;


    // Start is called before the first frame update
    void Start()
    {
        shipScroller.OnIndexChange += OnShipIndexChange;
        OnShipIndexChange(0);
        PrepareBattleScene();
    }

    private void OnDestroy()
    {
        shipScroller.OnIndexChange -= OnShipIndexChange;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: there's no GetButtonDown on InputMgr
        if (InputMgr.GetKeyDown(1, InputMgr.eButton.ATTACK))
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeSelected();
        }
        if (InputMgr.GetKeyDown(1, InputMgr.eButton.CANCEL)) 
        //if (Input.GetKeyDown(KeyCode.M))
        {
            ExitUpgrade();
        }
    }

    // Upgrades selected ship-equipment
    private void UpgradeSelected()
    {
        //TODO: define best way to call the upgrade
        bool upgrade = PlayerSO.Instance.Upgrade(PlayerSO.Instance.playerData.inventories[shipScroller.currentIndex].equipments[equipmentScrollers[shipScroller.currentIndex].currentIndex]);
        //equipmentScrollers[shipScroller.currentIndex].transform.GetChild(equipmentScrollers[shipScroller.currentIndex].currentIndex).GetComponent<UIEqUpgrader>().equipmentData.Upgrade();
    }


    //private void SetUpgrade(int shipIndex, int equipmentIndex)
    //{
    //    equipmentScrollers[shipScroller.currentIndex].transform.GetChild(equipmentIndex);
    //}

    // Triggered by ship's index change event.
    private void OnShipIndexChange(int indexFrom)
    {
        // Ativa apenas os equipamentos da nave selecionada
        for (int i = 0; i < equipmentScrollers.Length; i++)
        {
            if (i == shipScroller.currentIndex)
            {
                equipmentScrollers[i].gameObject.SetActive(true);
                equipmentScrollers[i].currentIndex = equipmentScrollers[indexFrom].currentIndex;
            }
            else
            {
                equipmentScrollers[i].gameObject.SetActive(false);
            }
        }
    }

    private void ExitUpgrade()
    {
        print("Im here");
        //SceneManager.LoadScene("BattleFinal");
        GameConfig.Instance.SaveGame();
        asyncLoad.allowSceneActivation = true;
    }

    private void PrepareBattleScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("BattleFinal");
        asyncLoad.allowSceneActivation = false;
    }

    private IEnumerator LoadBattleAsync()
    {
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
