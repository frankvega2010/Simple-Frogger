﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<LogsCollision> woodLogs;
    public GameObject water;
    public GameObject player;
    public UILevelFinish finishUI;
    public float endLevelWaitingTime;

    private PlayerStatus playerStatus;
    private Collision playerCollision;
    private float timer;
    private bool hasLevelEnded;

    // Start is called before the first frame update
    private void Awake()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
        playerCollision = player.GetComponent<Collision>();
        playerCollision.OnPlayerPassedObstacle += GivePoints;
        playerCollision.OnPlayerTouchedLog = SwitchStatus;
        playerCollision.OnPlayerDeath += SubtractLives;
        playerCollision.OnPlayerExitCollision = CheckLogsCollision;
        ObstacleSpawner.OnSpawnerAddLog += AddLog;
        ObstacleSpawner.OnSpawnerRemoveLog += RemoveLog;
        LevelLimit.OnPlayerEndLevel = EndLevel;
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
        }
        else
        {
            finishUI.showUI("You Lost!", Color.red);
        }
    }

    private void GoToGameOverScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
