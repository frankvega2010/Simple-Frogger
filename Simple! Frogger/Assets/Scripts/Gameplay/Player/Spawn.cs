using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Collision.OnPlayerDeath = RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        transform.position = new Vector3(0, -4.26f, 0);
    }

    /*private void OnDestroy()
    {
        Debug.Log("OnDestroy1");
    }*/
}
