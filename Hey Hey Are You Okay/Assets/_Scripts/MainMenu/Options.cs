using UnityEngine;
using UnityEngine.UI;
using Sound;

public class Options : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;

    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

    public void ChangeBGMVolume()
    {
        AudioManager.Instance.SetVolume("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.Save();
    }
}
