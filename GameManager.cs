using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public int score;
    public int highScore;
    public int lastScore;
    [SerializeField]
    TextMeshProUGUI scoreText, highScoreText, lastScoreText;
    void Awake()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
       
        if (singleton == null) singleton = this;
        else if(singleton != this) Destroy(gameObject);              
    }
    private void Update()
    {
        scoreText.text = score.ToString();
        highScoreText.text = "High: " + highScore;
        lastScoreText.text = "Last: " + lastScore;
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score >= highScore) highScore = score;
        PlayerPrefs.SetInt("Highscore", highScore);
        
    }
}
