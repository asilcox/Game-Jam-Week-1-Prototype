using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text highscoreText;
    public int pointTotal = 0;

    int score = 0;
    int highscore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddPoint()
    {
        score += pointTotal;
        scoreText.text = score.ToString();
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddPoint();
        }

        if (score > highscore)
            highscoreText.text = "HIGHSCORE: " + score.ToString();
    }
}
