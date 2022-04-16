using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private int score;
    public Text scoreDisplay;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetInt("score");
        }
        else
        {
            score = 0;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreDisplay.text = score.ToString();
    }
}
