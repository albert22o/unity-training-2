using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
}