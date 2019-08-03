using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
    }
}
