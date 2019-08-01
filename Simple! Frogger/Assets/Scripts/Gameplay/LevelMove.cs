using UnityEngine;

public class LevelMove : MonoBehaviour
{
    public delegate void OnLevelAction();

    public OnLevelAction OnLevelMove;

    public Transform player;
    public LevelLimit levelLimit;
    public GameObject street;
    public GameObject lake;
    public float moveSpeed;
    public float distance;
    public float triggerDistance;

    private float finalDistance;
    public float currentDistance;
    private bool canMove;
    private bool doOnce;
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        levelLimit.OnPlayerTouch += StartMoving;
        finalDistance = street.transform.position.y - distance;
    }

    private void Update()
    {
        if (canMove)
        {
            if (!doOnce)
            {
                if (OnLevelMove != null)
                {
                    OnLevelMove();
                }

                doOnce = true;
            }

            street.transform.position = street.transform.position - new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            lake.transform.position = lake.transform.position - new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            player.position = player.position - new Vector3(0, moveSpeed, 0) * Time.deltaTime;

            if (street.transform.position.y <= finalDistance)
            {
                if (OnLevelMove != null)
                {
                    OnLevelMove();
                }

                collider.offset = collider.offset + new Vector2(0,triggerDistance);
                canMove = false;
                doOnce = false;
            }
        }
    }

    private void StartMoving()
    {
        canMove = true;
    }

    private void OnDestroy()
    {
        levelLimit.OnPlayerTouch -= StartMoving;
    }
}
