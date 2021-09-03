using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public float progress = 0f;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] GameObject helpText;

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
        if (ExamManager.Instance.isTutorial)
        {
            helpText.SetActive(true);
        }
    }

    void Update()
    {
        progressBar.SetCurrentFill(progress);
    }

    public void Progress(string name, ToolDrag tool, float increment = 0.5f)
    {
        if (progress <= 100)
        {
            progress += increment;
        }
        else
        {
            progress = 0;
            tool.OnForceEndDrag();
            EventManager.Instance.PlayEvent(name);
        }
    }

    public void FinishedSwipeEvent(string name)
    {
        progress = 0;
        EventManager.Instance.PlayEvent(name);
    }

    public void OnExitExams()
    {
        ExamManager.Instance.ResetExams();
    }
}
