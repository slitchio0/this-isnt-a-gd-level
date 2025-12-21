using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    void Start()
    {
        // Load saved values (or defaults)
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);

        ApplyVolume(volumeSlider.value);
    }

    public void SetVolume(float value)
    {
        if (AudioManager.Instance != null)
        AudioManager.Instance.SetVolume(value);
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }

    void ApplyVolume(float value)
    {
        AudioListener.volume = value;
    }
}