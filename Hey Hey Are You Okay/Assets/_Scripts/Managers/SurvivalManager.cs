using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sound;

public class SurvivalManager : MonoBehaviour
{
    public static SurvivalManager Instance { get; private set; }

    public string[] scenes;
    string[] savedScenes;
    int proceduresCompleted = 0;
    float totalAccuracy, timer = 30.0f, totalTime = 0.0f, tickingVolume = 0.1f, colorValue = 0.0f;
    [SerializeField] GameObject endPanel;
    [SerializeField] Text accuracyText, timerText, totalProceduresText, flawlessText, averageTimeText;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameStateManager.IsPlaying = true;
        GameStateManager.IsSurvival = true;
        AudioManager.Instance.Play("Ticking");
        StartCoroutine(LateStart(0.1f));
    }

    void Update()
    {
        if(timer <= 0)
        {
            EndSurvival();
        }
        else if (!GameStateManager.IsPaused)
        {
            timer -= Time.deltaTime;
            totalTime += Time.deltaTime;
            timerText.text = timer.ToString("F1");
        }

        tickingVolume = 0.6f - (timer / 60);
        AudioManager.Instance.SetVolume("Ticking", tickingVolume);

        colorValue = 1.0f - (timer / 30);
        if (UIManager.Instance != null)
            UIManager.Instance.SetAlphaVignette(colorValue);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        savedScenes = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            savedScenes[i] = string.Copy(scenes[i]);
        }
        StartProcedure();

        yield return new WaitForSeconds(waitTime);
        timerText.gameObject.SetActive(true);
    }

    public void StartProcedure()
    {
        GameStateManager.IsPaused = false;
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[rand]);
        timer = 30.0f;
    }

    public void EndSurvival()
    {
        GameStateManager.IsPaused = true;
        if(proceduresCompleted == 0)
        {
            accuracyText.text = "Accuracy Rate is 0.00%";
            averageTimeText.text = "Average Time per procedure is " + totalTime.ToString("F2") + "s";
        }
        else
        {
            accuracyText.text = "Accuracy Rate is " + (totalAccuracy / proceduresCompleted * 100).ToString("F2") + "%";
            averageTimeText.text = "Average Time per procedure is " + (totalTime / proceduresCompleted).ToString("F2") + "s";
        }
        totalProceduresText.text = proceduresCompleted.ToString();

        endPanel.SetActive(true);

        StartCoroutine(FadeInBG());
    }

    public void EndProcedure()
    {
        totalAccuracy += GameManager.Instance.accuracy;
        proceduresCompleted++;
        StartProcedure();
    }
    
    public void ResetSurvival()
    {
        proceduresCompleted = 0;
        totalAccuracy = 0;
        totalTime = 0;

        endPanel.SetActive(false);
    }

    IEnumerator FadeInBG()
    {
        Color curColor = endPanel.GetComponent<Image>().color;
        curColor.a = 0;
        foreach (Transform child in endPanel.transform)
        {
            if (child.TryGetComponent<Image>(out Image imageComponent))
                imageComponent.color = curColor;
            if (child.TryGetComponent<Text>(out Text textComponent))
                textComponent.color = new Vector4(0, 0, 0, curColor.a);
        }

        while (Mathf.Abs(curColor.a - 1.0f) > 0.001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            foreach (Transform child in endPanel.transform)
            {
                if (child.TryGetComponent<Image>(out Image imageComponent))
                    imageComponent.color = curColor;

                if (child.TryGetComponent<Text>(out Text textComponent))
                    textComponent.color = new Vector4(0, 0, 0, curColor.a);
            }
            yield return null;
        }
    }

    public void OnReplayButton()
    {
        ResetSurvival();
        StartProcedure();
    }

    public void OnLobbyButton()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
