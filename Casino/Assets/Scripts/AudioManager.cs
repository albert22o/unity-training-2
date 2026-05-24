using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip musicClip;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        SetSfxVolume(PlayerPrefs.GetFloat(SfxVolumeKey, 1f));
    }

    public void Start()
    {
        PlayMusic(musicClip, true);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public float GetMusicVolume() => musicSource.volume;

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public float GetSfxVolume() => sfxSource.volume;
}