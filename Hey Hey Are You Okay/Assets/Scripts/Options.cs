using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Toggle music;

    void Start()
    {
        if (AudioManager.Instance.IsPlaying("BGM"))
            music.isOn = true;
        else
            music.isOn = false;
    }

    public void ToggleMusic(bool toggle)
    {
        AudioManager.Instance.ToggleMusic(toggle);
    }
}
