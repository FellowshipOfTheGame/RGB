using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Leaderboard : MonoBehaviour
{
    public Text[] highScores;
    private int[] highScoreValues;
    private string[] highScoreNames;
    // referencia para o script do player, onde poderei pegar o scor
    // Start is called before the first frame update
    void Start()
    {
        highScoreValues = new int[highScores.Length];//Setando o tamanho do vetor de score para ser igual a quantidade de lugares na leaderboard
        highScoreNames = new string[highScores.Length];

        for (int x = 0; x < highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
            highScoreNames[x] = PlayerPrefs.GetString("highScoreNames" + x);
        }
        DrawScores();
    }

    //Essa funcao ira checar o novo score do player, para saber se deve entrar na leaderboard. Ela sera chamada no fim de cada sessao de jogo, por um outro script.
    public void CheckForHighScore()
    {
        int newScore = PlayerSO.Instance.playerData.Score;
        string newName = PlayerSO.Instance.playerData.Name;

        for (int i = 0; i < highScores.Length; i++)
        {
            if (newScore > highScoreValues[i])
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScoreValues[j] = highScoreValues[j - 1];
                    highScoreNames[j] = highScoreNames[j - 1];
                }
                highScoreValues[i] = newScore;
                highScoreNames[i] = newName;
                SaveScores();
                DrawScores();
                Debug.Log("Scores have been saved and drawn");
                break;
            }
        }
    }

    void SaveScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            PlayerPrefs.SetInt("highScoreValues" + x, highScoreValues[x]);
            PlayerPrefs.SetString("highScoreNames" + x, highScoreNames[x]);
        }
    }

    void DrawScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].text = highScoreNames[i] + ": " + highScoreValues[i].ToString();
        }
    }
}
