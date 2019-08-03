using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private enum LayerNames
    {
        Default = 0,
        Log = 10,
        maxLayers
    }

    public delegate void OnPlayerAction();
    public delegate void OnPlayerActionArgs(bool waterStatus);
    public OnPlayerAction OnPlayerDeath;
    public OnPlayerAction OnPlayerPassedObstacle;
    public OnPlayerAction OnPlayerExitCollision;
    public OnPlayerActionArgs OnPlayerTouchedLog;

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "log":
                if (OnPlayerTouchedLog != null)
                {
                    OnPlayerTouchedLog(false);
                }
                break;
            case "car":
                if (OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
                Debug.Log("Colision with CAR");
                break;
            case "water":
                    if (OnPlayerDeath != null)
                    {
                        OnPlayerDeath();
                    }
                    Debug.Log("Colision with WATER");
                break;
            case "points":
                if (OnPlayerPassedObstacle != null)
                {
                    OnPlayerPassedObstacle();
                }
                Debug.Log("Colision with points");
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "log")
        {
            if (OnPlayerExitCollision != null)
            {
                OnPlayerExitCollision();
            }
        }
    }
}
