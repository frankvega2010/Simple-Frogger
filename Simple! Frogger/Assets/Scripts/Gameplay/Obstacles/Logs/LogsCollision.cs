using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsCollision : MonoBehaviour
{
    public GameObject player;

    private Collision playerCollision;
    private Transform target;
    private ObstacleMovement.Directions choosenDirection;
    private int speed;
    private bool canMove;

    // Start is called before the first frame update
    private void Start()
    {
        playerCollision = player.GetComponent<Collision>();
        target = player.GetComponent<Transform>();
        speed = GetComponent<ObstacleMovement>().speed;
        choosenDirection = GetComponent<ObstacleMovement>().choosenDirection;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            switch (choosenDirection)
            {
                case ObstacleMovement.Directions.right:
                    target.position = target.position + new Vector3(speed, 0, 0) * Time.deltaTime;
                    break;
                case ObstacleMovement.Directions.left:
                    target.position = target.position - new Vector3(speed, 0, 0) * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMove = false;
        }
    }
}
