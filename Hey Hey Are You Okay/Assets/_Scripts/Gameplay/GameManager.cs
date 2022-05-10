using System.Collections;
using UnityEngine;
using Sound;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public float progress = 0f;
    public ToolDrag currentTool;
    float totalCorrect = 0, totalMistake = 0, totalMoves = 0;
    public float accuracy = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        AudioManager.Instance.Stop("BGM");
        AudioManager.Instance.SetVolume("Ticking", 0.1f);
        AudioManager.Instance.Play("Ticking");

        PersistentManager.Instance.isPlaying = true;
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UIManager.Instance.TextEventText.gameObject.SetActive(true);
    }

    void Update()
    {
        UIManager.Instance.ProgressBar.SetCurrentFill(progress);
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

            totalCorrect++;
            totalMoves++;

            if (ExamManager.Instance != null)
            {
                ExamManager.Instance.totalMoves++;
            }

            accuracy = (float)totalCorrect / (float)totalMoves * 1;

            EventManager.Instance.PlayEvent(name);
        }
    }

    public void MakeMistake()
    {
        currentTool.OnForceEndDrag();
        currentTool = null;

        totalMistake++;
        totalMoves++;

        if(ExamManager.Instance != null)
        {
            ExamManager.Instance.totalMistake++;
            ExamManager.Instance.totalMoves++;
            if (ExamManager.Instance.totalMistake >= 3)
            {
                ExamManager.Instance.EndExams();
            }
        }
        if (SurvivalManager.Instance != null)
            SurvivalManager.Instance.EndSurvival();

        accuracy = (float)totalCorrect / (float)totalMoves * 1;
        Debug.Log("Mistakes: " + totalMistake);
    }

    public void FinishSwipeEvent(string name, float accuracy, bool isLastSwipe)
    {
        progress = 0;

        if (isLastSwipe)
        {
            currentTool.OnForceEndDrag();
            currentTool = null;
        }

        totalCorrect += accuracy;
        totalMoves++;

        if (ExamManager.Instance != null)
        {
            ExamManager.Instance.totalMoves++;
        }

        EventManager.Instance.PlayEvent(name);
    }
}
