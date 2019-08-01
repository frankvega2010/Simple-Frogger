using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePointsCollider : MonoBehaviour
{
    public ObstacleSpawner spawner;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
           spawner.DeletePoints(gameObject);
        }
    }
}
