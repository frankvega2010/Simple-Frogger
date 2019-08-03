using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelButton : MonoBehaviour
{
    private GameObject savedStatusGO;
    private PlayerStatusSave savedStatus;

    // Start is called before the first frame update
    private void Start()
    {
        savedStatusGO = GameObject.Find("SavedStatus");
        savedStatus = savedStatusGO.GetComponent<PlayerStatusSave>();
    }

    public void GoToLevel()
    {
        if (savedStatus.isPlayerAlive)
        {
            SceneManager.LoadScene("Level" + (savedStatus.level+1));
        }
        else
        {
            SceneManager.LoadScene("Level" + savedStatus.level);
        }
    }
}
