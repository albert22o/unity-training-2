using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput; 
    [SerializeField] private int gameSceneIndex = 0;

    public void OnPlayButtonClick()
    {
        string playerName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName)) return;

        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameSceneIndex);
    }
}