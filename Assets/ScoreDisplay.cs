using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public Text moneyText;
    [Range(0, 1)]
    public float changeValueFactor;
    public int updateFramesInterval = 1;

    private float calculatedScore;
    private float calculatedMoney;

    private int displayedScore;
    private int displayedMoney;

    private string scoreBaseText;
    private string moneyBaseText;

    private void Awake()
    {
        //Player.Instance.OnValueChanged += UpdateText;
        scoreBaseText = scoreText.text;
        moneyBaseText = moneyText.text;
        if (updateFramesInterval <= 0)
        {
            updateFramesInterval = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        displayedScore = PlayerSO.Instance.playerData.Score;
        displayedMoney = PlayerSO.Instance.playerData.Money;
        calculatedScore = displayedScore;
        calculatedMoney = displayedMoney;
        scoreText.text += displayedScore;
        moneyText.text += displayedMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedScore != PlayerSO.Instance.playerData.Score || displayedMoney != PlayerSO.Instance.playerData.Money)
        {
            if (Time.frameCount % updateFramesInterval == 0)
            {
                UpdateText();
            }
        }
    }

    private void UpdateText()
    {
        calculatedScore = Lerp(calculatedScore, PlayerSO.Instance.playerData.Score, changeValueFactor);
        calculatedMoney = Lerp(calculatedMoney, PlayerSO.Instance.playerData.Money, changeValueFactor);
        int score = Mathf.RoundToInt(calculatedScore);
        int money = Mathf.RoundToInt(calculatedMoney);

        if (displayedScore != score)
        {
            displayedScore = score;
            GetComponent<AudioSource>().Play();
        }

        if (displayedMoney != money)
        {
            displayedMoney = money;
            GetComponent<AudioSource>().Play();
        }

        scoreText.text = scoreBaseText + displayedScore;
        moneyText.text = moneyBaseText + displayedMoney;

        
    }

    private float Lerp(float origin, int target, float factor)
    {
        float change = (target - origin) * factor;
        //if (Mathf.Abs(change) < 1)
        //{
        //    change = target > origin ? 1 : -1;
        //}        
        return origin + change;
    }


}
