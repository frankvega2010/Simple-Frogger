using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<LogsCollision> woodLogs;
    public GameObject water;
    public GameObject player;

    private PlayerStatus playerStatus;
    private Collision playerCollision;
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
}
