using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public bool isALevel;
    public bool assignLevelNumber;
    public int levelNumber;

    public void ChangeScene()
    {
        if (sceneName == "")
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        Time.timeScale = 1;

        if (isALevel)
        {
            if (assignLevelNumber)
            {
                PlayerStatusSave.Get().level = levelNumber;
            }

            LoaderManager.Get().LoadScene(sceneName);
            UILoadingScreen.Get().SetVisible(true);

        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}