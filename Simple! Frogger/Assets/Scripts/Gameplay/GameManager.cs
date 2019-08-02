using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }

    private void GivePoints()
    {
        playerStatus.score += 10;
    }

    private void SwitchStatus(bool waterStatus, int layer)
    {
        water.SetActive(waterStatus);
        player.layer = layer;
    }

    private void SubtractLives()
    {
        playerStatus.lives--;
    }

    private void OnDestroy()
    {
        playerCollision.OnPlayerDeath -= SubtractLives;
        playerCollision.OnPlayerPassedObstacle -= GivePoints;
    }
}
