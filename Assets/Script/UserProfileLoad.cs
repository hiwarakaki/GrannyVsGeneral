using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UserProfileLoad : MonoBehaviour
{
    public TextMeshProUGUI Username;
    public Image UserImage;
    void Start()
    {
        Username.text = UserProfile.Instance.userName;
        UserImage.sprite = UserProfile.Instance.profilePicture;

    }

}
