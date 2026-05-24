using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxSlider.value = AudioManager.Instance.GetSfxVolume();

        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSfxVolume);
    }
}