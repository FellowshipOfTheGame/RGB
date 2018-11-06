using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public static float Timer       { get; private set; }
    public static float DeltaTime   { get; private set; }

    public static bool IsPaused     { get; private set; }
    public static bool IsGameOver   { get; private set; }

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        IsPaused = false;
    }

    // ======================================================================================
    void Update()
    {
        if (!IsPaused)
        {
            DeltaTime = Time.deltaTime;
            Timer += DeltaTime;
        }
        else
        {
            DeltaTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //IsPaused = !IsPaused;
            if (IsPaused)
            {
                QuitGame();
            }
            else
            {
                IsPaused = true;
            }
        }
    }

    // ======================================================================================
    public void QuitGame()
    {
        Application.Quit();
    }

    // ======================================================================================
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
