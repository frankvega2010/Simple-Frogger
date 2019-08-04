using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    public enum idleDirections
    {
        left,
        right,
        back,
        front,
        maxDirs
    }


    public Animator animator;
    public int speed;
    public bool canMove;
    public LevelMove levelMove;
    public Vector3 verticalScreenLimit;
    public Vector3 horizontalScreenLimit;

    public bool changeAnimationsOnceX;
    public bool changeAnimationsOnceY;
    public idleDirections lastDirection;

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
        pos.x = Mathf.Clamp(pos.x, horizontalScreenLimit.x, horizontalScreenLimit.y); // 0.04f, 0.96f
        pos.y = Mathf.Clamp(pos.y, verticalScreenLimit.x, verticalScreenLimit.y); // 0.03f, 1f
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (XAxis > 0 && YAxis == 0)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

            if (!changeAnimationsOnceX)
            {
                lastDirection = idleDirections.right;
                SetAllAnimationsOnFalse();
                changeAnimationsOnceX = true;
            }
            
            animator.SetBool("right", true);
        }
        else if (XAxis < 0 && YAxis == 0)
        {
            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;

            if (!changeAnimationsOnceX)
            {
                lastDirection = idleDirections.left;
                SetAllAnimationsOnFalse();
                changeAnimationsOnceX = true;
            }

            animator.SetBool("left", true);
        }

        if (YAxis > 0 && XAxis == 0)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;

            if (!changeAnimationsOnceY)
            {
                lastDirection = idleDirections.front;
                SetAllAnimationsOnFalse();
                changeAnimationsOnceY = true;
            }

            animator.SetBool("front", true);
        }
        else if (YAxis < 0 && XAxis == 0)
        {
            transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;

            if (!changeAnimationsOnceY)
            {
                lastDirection = idleDirections.back;
                SetAllAnimationsOnFalse();
                changeAnimationsOnceY = true;
            }

            animator.SetBool("back", true);
        }

        if (XAxis == 0)
        {
            changeAnimationsOnceX = false;
            
        }

        if (YAxis == 0)
        {
            changeAnimationsOnceY = false;
        }

        if (YAxis == 0 && XAxis == 0)
        {
            SetAllAnimationsOnFalse();
            switch (lastDirection)
            {
                case idleDirections.back:
                    animator.SetBool("stopBack", true);
                    break;
                case idleDirections.front:
                    animator.SetBool("stopFront", true);
                    break;
                case idleDirections.left:
                    animator.SetBool("stopLeft", true);
                    break;
                case idleDirections.right:
                    animator.SetBool("stopRight", true);
                    break;
            }
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

    private void SetAllAnimationsOnFalse()
    {
        animator.SetBool("left",false);
        animator.SetBool("right", false);
        animator.SetBool("back", false);
        animator.SetBool("front", false);
        animator.SetBool("stopLeft", false);
        animator.SetBool("stopRight", false);
        animator.SetBool("stopFront", false);
        animator.SetBool("stopBack", false);
    }
}
