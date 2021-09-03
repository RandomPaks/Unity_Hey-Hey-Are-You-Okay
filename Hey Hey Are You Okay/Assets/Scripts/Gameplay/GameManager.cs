using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public float progress = 0f;
    [SerializeField] ProgressBar progressBar;

    public int level = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
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
        EventManager.Instance.PlayEvent(name);
        progress = 0;
    }
}
