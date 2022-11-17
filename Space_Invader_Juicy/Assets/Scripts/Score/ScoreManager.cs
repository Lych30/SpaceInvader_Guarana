using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager instance;
    public TextMeshProUGUI ScoreTMPRo;
    public TextMeshProUGUI HighScoreTMPRo;
    Animator ScoreAnimator;
    private int Score;
    private int HighScore;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
    private void Start()
    {
        ScoreAnimator = ScoreTMPRo.gameObject.GetComponent<Animator>();
        SetHighScoreText();
        UpdateScoreText();
    }
    public void AddScore(int AddingScore)
    {
        ScoreAnimator.Play("AddingScore");
        Score += AddingScore;
        CheckIfNewHighScore();
        UpdateScoreText();
    }
    public void UpdateScoreText()
    {
        ScoreTMPRo.color = new Color(1, 1 - ((float)Score / HighScore), 1 - ((float)Score / HighScore), 1);
        ScoreTMPRo.text = "Score : " + Score.ToString();
    }
    public void SetHighScoreText()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreTMPRo.text = "HighScore : " + HighScore.ToString();
    }
    public void CheckIfNewHighScore()
    {
        if(Score >= HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            HighScoreTMPRo.text = "HighScore : " + HighScore.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            HighScoreTMPRo.text = "HighScore : " + HighScore.ToString();
            SetHighScoreText();
            UpdateScoreText();
        }
    }
}
