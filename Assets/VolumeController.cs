using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject muteIcon;
    public GameObject unmuteIcon;

    private float lastVolume = 0.5f;

    void Start()
    {
        
    volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    volumeSlider.value = AudioListener.volume;

    UpdateIcons(AudioListener.volume);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;

        if (value > 0f)
            lastVolume = value;

        UpdateIcons(value);
    }

    public void ToggleMute()
    {
        if (AudioListener.volume > 0f)
        {
            volumeSlider.value = 0f;
        }
        else
        {
            volumeSlider.value = lastVolume > 0f ? lastVolume : 1f;
        }
    }

    private void UpdateIcons(float value)
    {
        bool isMuted = Mathf.Approximately(value, 0f);
        muteIcon.SetActive(isMuted);
        unmuteIcon.SetActive(!isMuted);
    }
}