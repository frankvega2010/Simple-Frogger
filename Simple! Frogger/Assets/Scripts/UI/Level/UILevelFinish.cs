using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelFinish : MonoBehaviour
{
    public GameObject finishPanel;
    public Text finishText;
    public Color textColor;

    // Start is called before the first frame update
    private void Start()
    {
        finishPanel.SetActive(false);
    }

    public void showUI(string text,Color color)
    {
        finishText.text = text;
        finishText.color = color;
        finishPanel.SetActive(true);
    }
}
