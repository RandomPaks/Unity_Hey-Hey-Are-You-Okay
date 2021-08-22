using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isWashing = false, isDrying = false, isApplying = false, isBandAiding = false;
    float progress = 0f;
    float inc = 5f;
    [SerializeField] ProgressBar progressBar;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Update()
    {
        if (isWashing) FirstAid(100, "BloodyToWater");
        if (isDrying) FirstAid(100, "WaterToDry");
        if (isApplying) FirstAid(100, "DryToBandAid");
        if (isBandAiding) FirstAid(100, "BandAidFinish");
        progressBar.SetCurrentFill(progress);
    }

    void FirstAid(float maxProgress, string name)
    {
        if (progress <= maxProgress)
        {
            progress += inc;
        }
        else
        {
            EventManager.Instance.PlayEvent(name);
            progress = 0;
        }
    }
}
