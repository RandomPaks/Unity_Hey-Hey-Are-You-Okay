using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public float progress = 0f;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] GameObject helpText;
    public ToolDrag currentTool;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        StartCoroutine(LateStart(0.1f));   
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        helpText.SetActive(true);
    }

    void Update()
    {
        progressBar.SetCurrentFill(progress);
    }

    bool isTaskDone => progress >= 100;

    public void Progress(string name, float increment = 0.5f)
    {
        if (!isTaskDone)
        {
            progress += increment;
        }
        else
        {
            progress = 0;
            currentTool.OnForceEndDrag();
            currentTool = null;
            EventManager.Instance.PlayEvent(name);
        }
    }

    public void FinishedSwipeEvent(string name)
    {
        progress = 0;
        EventManager.Instance.PlayEvent(name);
    }

    public void OnRestartLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
