using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoManager : MonoBehaviour
{
    public static bool debugMode = true;
    public bool resetLevels = true;
    public int startLevels = 1;
    public int weaponsLevel = 1;
    public float upgradeDelay = 3f;
    public List<WeaponSO> weapons = new List<WeaponSO>();
    public Text levelText;

    public UpgradeScreen upgradeScreen;

    private float timer = 0;

    public List<GameObject> ships;
    private int shipIndex = 0;


    private void Awake()
    {
        if (resetLevels)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < PlayerSO.Instance.playerData.inventories[i].equipments.Length; j++)
                {
                    PlayerSO.Instance.playerData.inventories[i].equipments[j].SetLevel(startLevels);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeapons();
        UpdateText();
        //foreach (GameObject s in ships)
        //{
        //    s.gameObject.SetActive(false);
        //}
        //ships[shipIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= upgradeDelay)
        {
            timer = 0;
            weaponsLevel++;
            if (weaponsLevel > 10)
            {
                weaponsLevel = 1;
            }
            UpdateWeapons();
            UpdateText();
        }
        //Input
        //if (Input.GetKeyDown (KeyCode.T)) //previous ship
        //{
        //    Transform transf = ships[shipIndex].transform;
        //    shipIndex--;
        //    if (shipIndex < 0)
        //    {
        //        shipIndex = ships.Count-1;
        //    }
        //    ChangeShip(transf);
        //}
        //if (Input.GetKeyDown (KeyCode.Y)) // next ship
        //{
        //    Transform transf = ships[shipIndex].transform;
        //    shipIndex++;
        //    if (shipIndex == ships.Count)
        //    {
        //        shipIndex = 0;  
        //    }
        //    ChangeShip(transf);
        //}
        if (Input.GetKeyDown(KeyCode.U))
        {
            upgradeScreen.gameObject.SetActive(!upgradeScreen.gameObject.activeInHierarchy);
        }
    }

    private void ChangeShip (Transform transf)
    {
        foreach(GameObject s in ships)
        {
            s.SetActive(false);
        }
        ships[shipIndex].SetActive(true);
        ships[shipIndex].transform.position = transf.position;
    }

    void UpdateText ()
    {
        levelText.text = "Weapons Level: " + weaponsLevel;
    }

    void UpdateWeapons()
    {
        foreach (WeaponSO w in weapons)
        {
            //w.level = weaponsLevel;
        }
    }
}
