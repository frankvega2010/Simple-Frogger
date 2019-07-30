using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private enum PlayerDirectionsX
    {
        left,
        right,
        maxDir
    }

    private enum PlayerDirectionsY
    {
        up,
        down,
        maxDir
    }

    public int speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxisRaw("Horizontal");
        float YAxis = Input.GetAxisRaw("Vertical");

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.04f, 0.96f);
        //pos.y = Mathf.Clamp(pos.y, 0.08f, 0.92f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (XAxis > 0 && YAxis == 0)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
        }
        else if (XAxis < 0 && YAxis == 0)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
        }

        if (YAxis > 0 && XAxis == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
        }
        else if (YAxis < 0 && XAxis == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
        }
    }
}
