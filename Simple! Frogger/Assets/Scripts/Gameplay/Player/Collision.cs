using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public delegate void OnPlayerAction();
    public OnPlayerAction OnPlayerDeath;
    public GameObject water;

    private PlayerStatus player;
    private string nextCollider;
    private bool firstCollision;


    private void Start()
    {
        player = GetComponent<PlayerStatus>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (nextCollider == null)
        {
            gameObject.layer = 0;
            water.SetActive(true);
        }

        if (firstCollision)
        {
            nextCollider = col.gameObject.tag;
        }

        switch (col.gameObject.tag)
        {
            case "log":
                gameObject.layer = 10;
                water.SetActive(false);
                firstCollision = true;
                Debug.Log("Colision with LOG");
                break;
            case "car":
                if (OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
                firstCollision = true;
                Debug.Log("Colision with CAR");
                break;
            case "water":
                if (gameObject.layer == 0)
                {
                    if (OnPlayerDeath != null)
                    {
                        OnPlayerDeath();
                    }
                    Debug.Log("Colision with WATER");
                }
                firstCollision = true;
                break;
            case "points":
                player.score = player.score + 10;
                //col.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Colision with something");
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (gameObject.layer == 10 && nextCollider != "log")
        {
            gameObject.layer = 0;
            water.SetActive(true);
            firstCollision = false;
        }
        nextCollider = null;
    }
}
