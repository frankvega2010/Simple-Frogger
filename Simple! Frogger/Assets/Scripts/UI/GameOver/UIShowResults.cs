using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowResults : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    public Text timeText;
    public Text levelButtonText;

    public bool onlyHighscore;
    public string time;
    public int score;

    private GameObject savedStatusGO;
    private PlayerStatusSave savedStatus;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);

        if (!onlyHighscore)
        {
            savedStatusGO = GameObject.Find("SavedStatus");
            savedStatus = savedStatusGO.GetComponent<PlayerStatusSave>();

            scoreText.text = "Score: " + savedStatus.score;
            timeText.text = savedStatus.time;

            if (savedStatus.isPlayerAlive)
            {
                levelButtonText.text = "Next Level!";
            }
            else
            {
                levelButtonText.text = "Try Again?";
            }
        }
    }
}
