using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Collision playerCollision;

    // Start is called before the first frame update
    private void Start()
    {
        playerCollision = GetComponent<Collision>();
        playerCollision.OnPlayerDeath = RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        transform.position = new Vector3(0, -4.26f, 0);
    }
}
