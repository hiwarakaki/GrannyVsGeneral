using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Google;

public class LoginManager : MonoBehaviour
{
    public Button guestLoginButton;
    public Button googleLoginButton;
    public TextMeshProUGUI userNameText; 
    public Image profileImage;
    public Sprite guessImage;
    public GameObject UserSet;
    public GameObject loginSet;
    public GameObject MainMenuSet;

    private GoogleSignInConfiguration configuration;

    void Start()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = "973455493933-rcbo8eu1iv62ha20eqn7pgd3ea4fcj2p.apps.googleusercontent.com",
            RequestEmail = true,
            RequestIdToken = true
        };

        guestLoginButton.onClick.AddListener(GuestLogin);
        googleLoginButton.onClick.AddListener(GoogleLogin);
    }

    void GuestLogin()
    {
        Debug.Log("Guest Login");
        userNameText.text = "Guest";
        profileImage.sprite = guessImage;
        UserProfile.Instance.SetUserProfile("Guess", guessImage);
        UserSet.SetActive(true);
        loginSet.SetActive(false);
        MainMenuSet.SetActive(true);
    }

    void GoogleLogin()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthFinished);
    }

    private void OnGoogleAuthFinished(System.Threading.Tasks.Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google Login Failed: " + task.Exception.Message);
        }
        else if (task.IsCanceled)
        {
            Debug.LogWarning("Google Login Canceled");
        }
        else
        {
            GoogleSignInUser user = task.Result;
            Debug.Log("Google Login Success: " + user.DisplayName);

            userNameText.text = user.DisplayName;
            StartCoroutine(LoadProfileImage(user.ImageUrl.AbsoluteUri,user.DisplayName));
        }
    }

    private System.Collections.IEnumerator LoadProfileImage(string url, string userName)
    {
        if (string.IsNullOrEmpty(url)) yield break;

        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || request.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error Loading Profile Image: " + request.error);
            }
            else
            {
                Texture2D texture = ((UnityEngine.Networking.DownloadHandlerTexture)request.downloadHandler).texture;
                profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                profileImage.gameObject.SetActive(true);

                UserProfile.Instance.SetUserProfile(userName, profileImage.sprite);
                UserSet.SetActive(true);
                loginSet.SetActive(false);
                MainMenuSet.SetActive(true);
            }
        }
    }
}
