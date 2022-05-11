using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public static bool IsTraining = false;
    public static bool IsExam = false;
    public static bool IsSurvival = false;
    public static bool IsPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
