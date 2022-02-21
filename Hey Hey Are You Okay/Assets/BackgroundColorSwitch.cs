using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorSwitch : MonoBehaviour
{
    public static BackgroundColorSwitch Instance { get; private set; }

    [SerializeField] Gradient gradient;
    Image bg;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        bg = GetComponent<Image>();
    }

    public void SetImageGradient(float value)
    {
        bg.color = gradient.Evaluate(value);
    }
}
