using UnityEngine;
using UnityEngine.UI;

public class VignetteColorSwitch : MonoBehaviour
{
    public static VignetteColorSwitch Instance { get; private set; }

    Image bg;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        bg = GetComponent<Image>();
    }

    public void SetAlphaVignette(float value)
    {
        Color color = Color.white;
        color.a = value;
        bg.color = color;
    }
}
