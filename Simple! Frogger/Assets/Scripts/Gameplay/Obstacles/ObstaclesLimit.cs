using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLimit : MonoBehaviour
{
    public delegate void OnObstacleAction(GameObject obstacle);

    public OnObstacleAction OnObstacleEnter;

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "car":
                if (OnObstacleEnter != null)
                {
                    OnObstacleEnter(col.gameObject);
                }
                break;
            default:
                break;
        }
    }
}
