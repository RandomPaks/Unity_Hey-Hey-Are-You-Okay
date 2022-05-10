using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public ProgressBar ProgressBar;

    public GameObject TextEventBGObject;
    public Text TextEventText;

    [SerializeField] private Image _vignetteBG;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Image lobbyButtonImage;
    [SerializeField] private Sprite lobbyButtonSprite;

    [SerializeField] private GameObject _endPanel;

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

    private void Start()
    {
        if (GameStateManager.IsExam || GameStateManager.IsSurvival)
        {
            lobbyButtonImage.sprite = lobbyButtonSprite;
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
        _endPanel.SetActive(true);
        StartCoroutine(FadeInEndPanel());
    }

    IEnumerator FadeInEndPanel()
    {
        Color curColor = new Color(1, 1, 1, 0);
        List<Image> EndPanelImages = new List<Image>(_endPanel.GetComponentsInChildren<Image>());
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

    public void OnLoadScene(string scene)
    {
        if (GameStateManager.IsExam)
        {
            ExamManager.Instance.ResetExams();
            ExamManager.Instance.StartProcedure();
        }
        else if (GameStateManager.IsSurvival)
        {
            SurvivalManager.Instance.ResetSurvival();
            SurvivalManager.Instance.StartProcedure();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void OnLobbyButton()
    {
        SceneManager.LoadScene("MainMenu");
        if (GameStateManager.IsExam)
        {
            Destroy(ExamManager.Instance.gameObject);
        }
        else if (GameStateManager.IsSurvival)
        {
            Destroy(SurvivalManager.Instance.gameObject);
        }
    }

    public void OnPauseButton()
    {
        if(!GameStateManager.IsPaused)
        {
            _endPanel.SetActive(true);
            GameStateManager.IsPaused = true;
        }
        else
        {
            _endPanel.SetActive(false);
            GameStateManager.IsPaused = false;
        }
    }
}
