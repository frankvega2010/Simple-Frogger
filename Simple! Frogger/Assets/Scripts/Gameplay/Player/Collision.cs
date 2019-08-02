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

    public string lastCollider;
    public string nextCollider;
    public bool firstCollision;

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

                if (lastCollider == null)
                {
                    lastCollider = col.gameObject.tag;
                }
                else
                {
                    nextCollider = col.gameObject.tag;
                    Debug.Log(nextCollider);
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
        if (other.gameObject.tag == "log")
        {
            if (gameObject.layer == (int)LayerNames.Log && nextCollider != "log")
            {
                if (OnPlayerTouchedLog != null)
                {
                    OnPlayerTouchedLog(true, (int)LayerNames.Default);
                }
                firstCollision = false;

                lastCollider = null;
                nextCollider = null;
            }
            lastCollider = nextCollider;
            nextCollider = null;
        }
    }
}
