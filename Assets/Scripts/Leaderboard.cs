using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Leaderboard : MonoBehaviour
{
    public Text[] highScores;
    private int[] highScoreValues;
    // referencia para o script do player, onde poderei pegar o score
    private PlayerSO player;
    // Start is called before the first frame update
    void Start()
    {

        player = PlayerSO.Instance;
        highScoreValues = new int[highScores.Length];//Setando o tamanho do vetor de score para ser igual a quantidade de lugares na leaderboard

        for (int x = 0; x < highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
        }
        DrawScores();
    }

    //Essa funcao ira checar o novo score do player, para saber se deve entrar na leaderboard. Ela sera chamada no fim de cada sessao de jogo, por um outro script.
    public void CheckForHighScore()
    {
        int newScore = player.playerData.Score;
        for (int i = 0; i < highScores.Length; i++)
        {
            if (newScore > highScoreValues[i])
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScoreValues[j] = highScoreValues[j - 1];
                }
                highScoreValues[i] = newScore;
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
        }
    }

    void DrawScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].text = "Position " + (i+1).ToString() + ": " + highScoreValues[i].ToString();
        }
    }
}
