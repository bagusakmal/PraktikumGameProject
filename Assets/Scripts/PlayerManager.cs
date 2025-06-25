using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject StartText;
    public TMP_Text HighScoreText;

    public static int numberOfCoincs;
    private int highScore;
    public TMP_Text skorText;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoincs = 0;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        HighScoreText.text = "Highscore: " + highScore *5;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            if(highScore < numberOfCoincs)
            {
                PlayerPrefs.SetInt("highScore", numberOfCoincs);
            }
            
            gameOverPanel.SetActive(true);
            
        }
        skorText.text = "Skor: " + numberOfCoincs * 5;
        if (Input.GetKeyDown(KeyCode.Mouse0) || SweepManager.tap)
        {
            isGameStarted = true;
            Destroy(HighScoreText);
            Destroy(StartText);
        }
    }
}
