using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoManager : MonoBehaviour
{
    public int weaponsLevel = 1;
    public float upgradeDelay = 3f;
    public List<WeaponSO> weapons = new List<WeaponSO>();
    public Text levelText;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeapons();
        UpdateText();
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
    }

    void UpdateText ()
    {
        levelText.text = "Weapons Level: " + weaponsLevel;
    }

    void UpdateWeapons()
    {
        foreach (WeaponSO w in weapons)
        {
            w.level = weaponsLevel;
        }
    }
}
