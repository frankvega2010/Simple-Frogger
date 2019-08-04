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
            CurrentSessionStats.Get().level += 1;
            LoaderManager.Get().LoadScene("Level" + (savedStatus.level + 1));
            UILoadingScreen.Get().SetVisible(true);
        }
        else
        {
            LoaderManager.Get().LoadScene("Level" + savedStatus.level);
            UILoadingScreen.Get().SetVisible(true);
        }
    }
}
