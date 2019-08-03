using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLimit : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "car":
                if (obstacleSpawner.newObstacle == col.gameObject)
                {
                    obstacleSpawner.RemoveAndSpawn();
                }
                break;
            case "log":
                if (obstacleSpawner.newObstacle == col.gameObject)
                {
                    obstacleSpawner.RemoveAndSpawn();
                }
                break;
            default:
                break;
        }
    }
}
