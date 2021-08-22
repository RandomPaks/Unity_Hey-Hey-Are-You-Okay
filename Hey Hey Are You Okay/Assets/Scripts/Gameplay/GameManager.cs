using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isWashing = false;
    float progress = 0f;
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
        if (isWashing)
            Washing();
    }

    void Washing()
    {
        if(progress < 100)
        {
            progress += 0.25f;
            progressBar.SetCurrentFill(progress);
        }
    }
}
