using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public GameObject player;
    public Text timeText;
    public Text livesText;
    public Text pointsText;
    public Text levelText;

    private PlayerStatus playerStatus;
    private Collision playerCollision;
    private float timer;
    private string time;
    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    private void Start()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
        playerCollision = player.GetComponent<Collision>();

        playerCollision.OnPlayerDeath += UpdateUILives;
        playerCollision.OnPlayerPassedObstacle += UpdateUIPoints;
        UpdateUILives();
        UpdateUIPoints();
        UpdateUILevel();
    }

    private void Update()
    {
        UpdateUITime();
    }


    private void UpdateUILives()
    {
        livesText.text = "x" + playerStatus.lives;
    }

    private void UpdateUIPoints()
    {
        pointsText.text = "Points: " + playerStatus.score;
    }

    private void UpdateUILevel()
    {
        levelText.text = "Level: " + playerStatus.level;
    }

    private void UpdateUITime()
    {
        timer += Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = "Time: " + time;
    }

    private void OnDestroy()
    {
        playerCollision.OnPlayerDeath -= UpdateUILives;
        playerCollision.OnPlayerPassedObstacle -= UpdateUIPoints;
    }
}
