using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleSpawner : MonoBehaviour
{
    public delegate void OnSpawnerAction(LogsCollision log);

    public static OnSpawnerAction OnSpawnerAddLog;
    public static OnSpawnerAction OnSpawnerRemoveLog;

    public GameObject parent;
    public GameObject obstacleBase;
    public ObstaclesLimit limit;
    public LevelMove levelMove;
    public float minSpeed;
    public float maxSpeed;
    public bool isSecondRow;

    public GameObject newObstacle;
    public float randomSpeed;
    private LogsCollision savedLogsCollision;

    // Start is called before the first frame update
    private void Start()
    {
        levelMove.OnLevelMove += RemoveOnly;
        levelMove.OnLevelFinishedMoving += Spawn;
        Spawn();
    }

    private void RemoveOnly()
    {
        if (gameObject.tag == "log")
        {
            if (OnSpawnerRemoveLog != null)
            {
                OnSpawnerRemoveLog(savedLogsCollision);
            }
        }
        Destroy(newObstacle);
    }

    private void OnDestroy()
    {
        levelMove.OnLevelMove -= RemoveOnly;
        levelMove.OnLevelFinishedMoving -= Spawn;
    }

    public void RemoveAndSpawn()
    {
        RemoveOnly();
        Spawn();
    }

    private void Spawn()
    {
        newObstacle = Instantiate(obstacleBase);
        newObstacle.transform.SetParent(parent.transform, false);
        randomSpeed = Random.Range(minSpeed, maxSpeed);
        newObstacle.GetComponent<ObstacleMovement>().speed = randomSpeed;
        newObstacle.SetActive(true);
        if (gameObject.tag == "log")
        {
            if (OnSpawnerAddLog != null)
            {
                OnSpawnerAddLog(newObstacle.GetComponent<LogsCollision>());
                savedLogsCollision = newObstacle.GetComponent<LogsCollision>();
            }
        }
    }

    public void DeletePoints(GameObject pointsGameObject)
    {
        if (!isSecondRow)
        {
            Transform child = obstacleBase.transform.GetChild(0);
            Destroy(child.gameObject);
            Destroy(pointsGameObject);
        }
    }
}
