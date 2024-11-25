using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int currentTurn = 0;
    public bool Skillable = true;
    public GameObject Grannyin;
    public GameObject Generalin;
    public bool gameEndALD;

    public GameObject EndUI;
    public GameObject ShareUI;

    public ChargeThrow granny;
    public ChargeThrow general;
    public string winText;
    public TextMeshProUGUI winTextBox;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeTurn()
    {
        if (currentTurn == 0)
        {
            currentTurn = 1;
            Grannyin.SetActive(true);
            Generalin.SetActive(false);
        }
        else
        {
            currentTurn = 0;
            Grannyin.SetActive(false);
            Generalin.SetActive(true);
        }
        Skillable = true;
    }

    public void gameEnd()
    {
        gameEndALD = true;
        general.turnEnd = true;
        granny.turnEnd = true;
        SkeletonAnimation GrannyskeletonAnimation = granny.GetComponent<SkeletonAnimation>();
        SkeletonAnimation GeneralskeletonAnimation = granny.GetComponent<SkeletonAnimation>();
        if (general.PlayerHealth <= 0)
        {
            winText = "Granny Won!";
            winTextBox.text = winText;
            GrannyskeletonAnimation.AnimationState.SetAnimation(0, "Cheer Friendly", false); // เล่น Animation "normal_hit"
            GeneralskeletonAnimation.AnimationState.SetAnimation(0, "Moody UnFriendly", true); // กลับไปเล่น "idle" หลังจากจบ
        }
        else
        {
            winText = "General Won!";
            winTextBox.text = winText;
            GrannyskeletonAnimation.AnimationState.SetAnimation(0, "Moody UnFriendly", true); // เล่น Animation "normal_hit"
            GeneralskeletonAnimation.AnimationState.SetAnimation(0, "Cheer Friendly", true); // กลับไปเล่น "idle" หลังจากจบ
        }
        EndUI.SetActive(true);
    }

    public void Sharebutton()
    {
        ShareUI.SetActive(true);
    }

    public void ShareOnFacebook()
    {
        string result = "I Winn!!";
        string url = "https://www.facebook.com/sharer/sharer.php?u=" + UnityEngine.Networking.UnityWebRequest.EscapeURL("https://example.com") + "&t=" + UnityEngine.Networking.UnityWebRequest.EscapeURL(result);
        Application.OpenURL(url);
    }

    public void ShareOnLine()
    {
        string result = "I Winn!!";
        string url = "line://msg/text/" + UnityEngine.Networking.UnityWebRequest.EscapeURL(result);
        Application.OpenURL(url);
    }

    public void ShareOnTwitter()
    {
        string result = "I Winn!!";
        string url = "https://twitter.com/intent/tweet?text=" + UnityEngine.Networking.UnityWebRequest.EscapeURL(result);
        Application.OpenURL(url);
    }

    public void CloseSharetab()
    {
        ShareUI.SetActive(false);
    }

}
