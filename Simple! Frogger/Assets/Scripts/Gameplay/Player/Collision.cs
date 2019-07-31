using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public delegate void OnPlayerAction();

    public static OnPlayerAction OnPlayerDeath;

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "car":
                if (OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
                Debug.Log("Colision with CAR");
                break;
            default:
                Debug.Log("Colision with something");
                break;
        }
    }
}
