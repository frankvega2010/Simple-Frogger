using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLimit : MonoBehaviour
{
    public delegate void OnPlayerAction();

    public OnPlayerAction OnPlayerTouch;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (OnPlayerTouch != null)
            {
                OnPlayerTouch();
            }
        }
        
    }
}
