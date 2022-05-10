using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static ProgressBar ProgressBar;

    public static GameObject TextEventBGObject;
    public static Text TextEventText;

    public static GameObject EndPanel;

    [SerializeField] private static Image _vignetteBG;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public static void SetAlphaVignette(float value)
    {
        Color color = Color.white;
        color.a = value;
        _vignetteBG.color = color;
    }

    public static IEnumerator FadeInEndPanel()
    {
        Color curColor = EndPanel.GetComponent<Image>().color;
        while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            foreach (Transform child in EndPanel.transform)
            {
                if (child.TryGetComponent(out Image image))
                {
                    image.color = curColor;
                }
            }
            yield return null;
        }
    }
}
