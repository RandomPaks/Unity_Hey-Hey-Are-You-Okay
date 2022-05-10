using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public ProgressBar ProgressBar;

    [Space(10)]
    [SerializeField] private Image _vignetteBG;

    [Header("Dialogue")]
    public GameObject TextEventBGObject;
    public Text TextEventText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Image lobbyButtonImage;
    [SerializeField] private Sprite lobbyButtonSprite;

    [Header("End Menu")]
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

    private IEnumerator FadeInEndPanel()
    {
        Color curColor = new Color(1, 1, 1, 0);
        Image[] EndPanelImages = _endPanel.GetComponentsInChildren<Image>();
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

    public IEnumerator ShakeTextEventBG()
    {
        Vector3 startPos = TextEventBGObject.transform.position;
        float timer = 0f;

        while (timer < 0.15f)
        {
            timer += Time.deltaTime;
            TextEventBGObject.transform.position = startPos + (Random.insideUnitSphere * 15);
            yield return new WaitForSeconds(0.02f);
        }

        TextEventBGObject.transform.position = startPos;
        yield return null;
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
            _pausePanel.SetActive(true);
            GameStateManager.IsPaused = true;
        }
        else
        {
            _pausePanel.SetActive(false);
            GameStateManager.IsPaused = false;
        }
    }
}
