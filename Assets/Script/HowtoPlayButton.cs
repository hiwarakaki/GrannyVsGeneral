using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoPlayButton : MonoBehaviour
{
    public GameObject howtoplayUI;

    public void OpenHowtoplay()
    {
        howtoplayUI.SetActive(!howtoplayUI.activeSelf);
    }
}
