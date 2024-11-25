
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void loadnextscene()
    {
        SceneManager.LoadSceneAsync(1);
    }

}

