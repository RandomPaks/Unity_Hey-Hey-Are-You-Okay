using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public ProgressBar ProgressBar;

    public GameObject TextEventBGObject;
    public Text TextEventText;

    [SerializeField] private GameObject EndPanel;
    [SerializeField] private Image _vignetteBG;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetAlphaVignette(float value)
    {
        Color color = Color.white;
        color.a = value;
        _vignetteBG.color = color;
    }

    public void StartEndEvent()
    {
        EndPanel.SetActive(true);
        StartCoroutine(FadeInEndPanel());
    }

    IEnumerator FadeInEndPanel()
    {
        //Color curColor = EndPanel.GetComponent<Image>().color;
        //while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
        //{
        //    curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
        //    foreach (Transform child in EndPanel.transform)
        //    {
        //        if (child.TryGetComponent(out Image image))
        //        {
        //            image.color = curColor;
        //        }
        //    }
        //    yield return null;
        //}

        Color curColor = new Color(1, 1, 1, 0);
        List<Image> EndPanelImages = new List<Image>(EndPanel.GetComponentsInChildren<Image>());
        while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            foreach (Image image in EndPanelImages)
            {
                image.color = curColor;
            }
            yield return null;
        }
    }
}
