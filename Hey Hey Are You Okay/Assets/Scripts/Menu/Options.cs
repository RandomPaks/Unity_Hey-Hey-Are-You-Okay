using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 0.35f);
        }
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

    public void ChangeBGMVolume()
    {
        AudioManager.Instance.SetVolume("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
    }
}
