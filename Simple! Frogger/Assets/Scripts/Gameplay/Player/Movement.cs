using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int speed;
    public bool canMove;
    public LevelMove levelMove;

    private void Start()
    {
        levelMove.OnLevelMove += SwitchMovement;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        float XAxis = Input.GetAxisRaw("Horizontal");
        float YAxis = Input.GetAxisRaw("Vertical");

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.04f, 0.96f);
        pos.y = Mathf.Clamp(pos.y, 0.03f, 1f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (XAxis > 0 && YAxis == 0)
        {
            transform.position = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        else if (XAxis < 0 && YAxis == 0)
        {
            transform.position = transform.position - new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        if (YAxis > 0 && XAxis == 0)
        {
            transform.position = transform.position + new Vector3(0, speed, 0) * Time.deltaTime;
        }
        else if (YAxis < 0 && XAxis == 0)
        {
            transform.position = transform.position - new Vector3(0, speed, 0) * Time.deltaTime;
        }
    }

    private void SwitchMovement()
    {
        canMove = !canMove;
    }

    private void OnDestroy()
    {
        levelMove.OnLevelMove -= SwitchMovement;
    }
}
