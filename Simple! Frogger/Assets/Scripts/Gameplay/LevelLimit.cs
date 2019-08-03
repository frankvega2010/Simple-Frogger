using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLimit : MonoBehaviour
{
    public delegate void OnPlayerAction();

    public OnPlayerAction OnPlayerTouch;
    public static OnPlayerAction OnPlayerEndLevel;
    private bool hasPlayerFinishedLevel;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!hasPlayerFinishedLevel)
            {
                if (OnPlayerTouch != null)
                {
                    OnPlayerTouch();
                }
                hasPlayerFinishedLevel = true;
            }
            else
            {
                if (OnPlayerEndLevel != null)
                {
                    OnPlayerEndLevel();
                }

                Debug.Log("You won!");
            }
            
        }
    }
}
