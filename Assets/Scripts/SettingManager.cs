using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource bgmSource;

    void Start()
    {
        // Temukan AudioSource BGM
        GameObject bgmObj = GameObject.Find("BgMusic");
        if (bgmObj != null)
        {
            bgmSource = bgmObj.GetComponentInChildren<AudioSource>();
        }

        // Ambil volume dari PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        volumeSlider.value = savedVolume;

        if (bgmSource != null)
        {
            bgmSource.volume = savedVolume;
        }

        // Tambahkan listener ke slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }

        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
}
