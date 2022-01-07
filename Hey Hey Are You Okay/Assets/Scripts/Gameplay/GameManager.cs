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

            totalCorrect++;
            totalMoves++;

            accuracy = (float)totalCorrect / (float)totalMoves * 1;
            Debug.Log("Correct: " + totalCorrect);

            EventManager.Instance.PlayEvent(name);
        }
    }

    public void MakeMistake()
    {
        currentTool.OnForceEndDrag();
        currentTool = null;

        totalMistake++;
        totalMoves++;

        accuracy = (float)totalCorrect / (float)totalMoves * 1;
        Debug.Log("Mistakes: " + totalMistake);
    }

    public void FinishSwipeEvent(string name, float accuracy)
    {
        progress = 0; 
        currentTool.OnForceEndDrag();
        currentTool = null;

        totalCorrect += accuracy;
        totalMoves++;
        Debug.Log("Correct: " + totalCorrect);

        EventManager.Instance.PlayEvent(name);
    }
}
