using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isWashing = false, isDrying = false, isApplying = false, isBandAiding = false, isCalling = false;

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
        if(level == 1)
        { 
            if (isWashing) FirstAid("BloodyToWater", 0.25f);
            if (isBandAiding) FirstAid("BandAidFinish");
        }
        else if(level == 2)
        {
            if (isDrying) FirstAid("BloodToTowel", 0.25f);
        }
        progressBar.SetCurrentFill(progress);
    }

    void FirstAid(string name, float increment = 0.5f)
    {
        if (progress <= 100)
        {
            progress += increment;
        }
        else
        {
            EventManager.Instance.PlayEvent(name);
            progress = 0;
        }
    }

    public void FinishedSwipeEvent(string name)
    {
        EventManager.Instance.PlayEvent(name);
        progress = 0;
    }
}
