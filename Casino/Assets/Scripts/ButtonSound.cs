using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        if (AudioManager.Instance != null) 
            AudioManager.Instance.PlaySfx(audioClip);
    }
}