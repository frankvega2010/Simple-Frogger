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
    public delegate void OnPlayerActionArgs(bool waterStatus, int layer);
    public OnPlayerAction OnPlayerDeath;
    public OnPlayerAction OnPlayerPassedObstacle;
    public OnPlayerActionArgs OnPlayerTouchedLog;

    private string nextCollider;
    private bool firstCollision;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (nextCollider == null)
        {
            if (OnPlayerTouchedLog != null)
            {
                OnPlayerTouchedLog(true, (int)LayerNames.Default);
            }
        }

        if (firstCollision)
        {
            nextCollider = col.gameObject.tag;
        }

        switch (col.gameObject.tag)
        {
            case "log":
                if (OnPlayerTouchedLog != null)
                {
                    OnPlayerTouchedLog(false, (int)LayerNames.Log);
                }
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
                if (gameObject.layer == (int)LayerNames.Default)
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
        if (gameObject.layer == (int)LayerNames.Log && nextCollider != "log")
        {
            if (OnPlayerTouchedLog != null)
            {
                OnPlayerTouchedLog(true, (int)LayerNames.Default);
            }
            firstCollision = false;
        }
        nextCollider = null;
    }
}
