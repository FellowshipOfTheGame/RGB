using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{

    private void Start()
    {
       
    }

    public void SetPlayerName(string name)
    {
        PlayerSO.Instance.playerData.Name = name;
    }
}
