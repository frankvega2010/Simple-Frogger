using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusSave : MonoBehaviourSingleton<PlayerStatusSave>
{
    public int score;
    public int level;
    public string time;
    public bool isPlayerAlive;
}
