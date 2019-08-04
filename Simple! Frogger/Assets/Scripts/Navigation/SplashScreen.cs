using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Image[] logos;
    public float logoLifespanSpeed;
    public string sceneName;
    public float waitingTimeForNextScene;

    private float lifespanTimer;
    private float nextSceneTimer;
    private bool canGoReverse;
    private bool canEnd;
    private int logoIndex;

    // Start is called before the first frame update
    void Start()
    {
        logoLifespanSpeed = logoLifespanSpeed * 0.1f;
        logoIndex = 0;

        for (int i = 0; i < logos.Length; i++)
        {
            logos[i].color = new Vector4(1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canGoReverse)
        {
            lifespanTimer -= Time.deltaTime * logoLifespanSpeed;
        }
        else
        {
            lifespanTimer += Time.deltaTime * logoLifespanSpeed;
        }

        if (!canEnd)
        {
            logos[logoIndex].color = new Vector4(1, 1, 1, lifespanTimer);
        }

        if (lifespanTimer >= 1.0f)
        {
            canGoReverse = true;
        }
        else if (lifespanTimer < 0)
        {
            canGoReverse = false;
            logoIndex++;
            if (logoIndex >= logos.Length)
            {
                canEnd = true;
            }
        }

        if (canEnd)
        {
            nextSceneTimer += Time.deltaTime;

            if (nextSceneTimer >= waitingTimeForNextScene)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
