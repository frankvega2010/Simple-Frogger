using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject parent;
    public GameObject obstacleBase;
    public ObstaclesLimit limit;
    public LevelMove levelMove;

    private GameObject newCar;
    // Start is called before the first frame update
    private void Start()
    {
        limit.OnObstacleEnter = RemoveAndSpawn;
        levelMove.OnLevelMove += RemoveOnly;
        newCar = Instantiate(obstacleBase);
        newCar.transform.SetParent(parent.transform);
        newCar.SetActive(true);
    }

    private void RemoveAndSpawn(GameObject obstacle)
    {
        Destroy(obstacle);
        newCar = Instantiate(obstacleBase);
        newCar.transform.SetParent(parent.transform);
        newCar.SetActive(true);
    }

    private void RemoveOnly()
    {
        Destroy(newCar);
    }

    private void OnDestroy()
    {
        levelMove.OnLevelMove -= RemoveOnly;
    }

    public void DeletePoints(GameObject pointsGameObject)
    {
        Transform child = obstacleBase.transform.GetChild(0);
        Destroy(child.gameObject);
        Destroy(pointsGameObject);
    }
}
