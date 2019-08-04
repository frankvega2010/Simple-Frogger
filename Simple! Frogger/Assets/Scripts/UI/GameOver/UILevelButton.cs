using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelButton : MonoBehaviour
{
    public int maxLevels;
    private PlayerStatusSave savedStatus;
    
    // Start is called before the first frame update
    private void Start()
    {
        savedStatus = PlayerStatusSave.Get();
    }

    public void GoToLevel()
    {
        if (savedStatus.isPlayerAlive)
        {
            savedStatus.level++;
            if (savedStatus.level > maxLevels)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                LoaderManager.Get().LoadScene("Level" + (savedStatus.level));
                UILoadingScreen.Get().SetVisible(true);
            }
            
        }
        else
        {
            LoaderManager.Get().LoadScene("Level" + savedStatus.level);
            UILoadingScreen.Get().SetVisible(true);
        }
    }
}
