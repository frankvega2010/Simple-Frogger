using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLink : MonoBehaviour
{
    public string link;

    public void GoToThisLink()
    {
        Application.OpenURL(link);
    }
}
