using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public enum Directions
    {
        left,
        right,
        maxDirs
    }

    public int speed;
    public Directions choosenDirection;

    // Update is called once per frame
    private void Update()
    {
        switch (choosenDirection)
        {
            case Directions.right:
                transform.position = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
                break;
            case Directions.left:
                transform.position = transform.position - new Vector3(speed, 0, 0) * Time.deltaTime;
                break;
            default:
                break;
        }
    }
}
