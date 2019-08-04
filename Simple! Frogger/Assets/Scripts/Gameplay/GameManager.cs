using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<LogsCollision> woodLogs;
    public Text timeText;
    public GameObject water;
    public GameObject player;
    public UILevelFinish finishUI;
    public float endLevelWaitingTime;

    private GameObject savedStatusGO;
    private PlayerStatusSave savedStatus;
    private PlayerStatus playerStatus;
    private Movement playerMovement;
    private Collision playerCollision;
    private float timer;
    private bool hasLevelEnded;

    // Start is called before the first frame update
    private void Awake()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
        playerCollision = player.GetComponent<Collision>();
        playerMovement = player.GetComponent<Movement>();
        playerCollision.OnPlayerPassedObstacle += GivePoints;
        playerCollision.OnPlayerTouchedLog = SwitchStatus;
        playerCollision.OnPlayerDeath += SubtractLives;
        playerCollision.OnPlayerExitCollision = CheckLogsCollision;
        ObstacleSpawner.OnSpawnerAddLog += AddLog;
        ObstacleSpawner.OnSpawnerRemoveLog += RemoveLog;
        LevelLimit.OnPlayerEndLevel = EndLevel;

        savedStatusGO = GameObject.Find("SavedStatus");
        savedStatus = savedStatusGO.GetComponent<PlayerStatusSave>();
    }

    private void Update()
    {
        if (hasLevelEnded)
        {
            timer += Time.deltaTime;
            if (timer >= endLevelWaitingTime)
            {
                GoToGameOverScene();
            }
        }
    }

    private void GivePoints()
    {
        playerStatus.score += 10;
    }

    private void SwitchStatus(bool waterStatus)
    {
        water.SetActive(waterStatus);
    }

    private void SubtractLives()
    {
        playerStatus.lives--;
        if (playerStatus.lives <= 0)
        {
            playerStatus.lives = 0;
            EndLevel();
        }
    }

    private void OnDestroy()
    {
        playerCollision.OnPlayerDeath -= SubtractLives;
        playerCollision.OnPlayerPassedObstacle -= GivePoints;
        ObstacleSpawner.OnSpawnerAddLog -= AddLog;
        ObstacleSpawner.OnSpawnerRemoveLog -= RemoveLog;
    }

    private void AddLog(LogsCollision log)
    {
        woodLogs.Add(log);
    }

    private void RemoveLog(LogsCollision log)
    {
        woodLogs.Remove(log);
    }

    private void CheckLogsCollision()
    {
        int amountFound = 0;

        for (int i = 0; i < woodLogs.Count; i++)
        {
            if (woodLogs[i].isPlayerOnIt)
            {
                amountFound++;
            }
        }

        if (amountFound > 0)
        {
            SwitchStatus(false);
        }
        else
        {
            SwitchStatus(true);
        }
    }

    private void EndLevel()
    {
        hasLevelEnded = true;
        if (playerStatus.lives > 0)
        {
            finishUI.showUI("You Won!", Color.green);
            savedStatus.isPlayerAlive = true;
        }
        else
        {
            finishUI.showUI("You Lost!", Color.red);
            savedStatus.isPlayerAlive = false;
        }

        playerMovement.enabled = false;
        savedStatus.score = playerStatus.score;
        savedStatus.time = timeText.text;
        UpdateHighscore();
        
    }

    private void GoToGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateHighscore()
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        int newHighscore = savedStatus.score;

        if (newHighscore >= oldHighscore)
        {
            PlayerPrefs.SetInt("highscore", newHighscore);
        }
    }
}
