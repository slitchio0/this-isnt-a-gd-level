using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        bgmSource.volume = volume;
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void PlayDeathSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}