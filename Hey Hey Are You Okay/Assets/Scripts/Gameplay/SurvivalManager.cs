using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SurvivalManager : MonoBehaviour
{
    public static SurvivalManager Instance { get; private set; }

    public string[] scenes;
    string[] savedScenes;
    float totalAccuracy, proceduresCompleted = 0;
    [SerializeField] GameObject endPanel;
    [SerializeField] Text accuracyText, timerText, totalProceduresText, flawlessText, averageTimeText;
    float timer = 30.0f, totalTime = 0.0f;

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
        PersistentManager.Instance.isSurvival = true;
        StartCoroutine(LateStart(0.1f));
    }

    void Update()
    {
        if(timer <= 0)
        {
            EndSurvival();
        }
        else if (!PersistentManager.Instance.isPaused)
        {
            timer -= Time.deltaTime;
            totalTime += Time.deltaTime;
            timerText.text = timer.ToString("F1");
        }
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
        PersistentManager.Instance.isPaused = false;
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[rand]);
        timer = 30.0f;
    }

    public void EndSurvival()
    {
        PersistentManager.Instance.isPaused = true;
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

    IEnumerator FadeInBG()
    {
        Color curColor = endPanel.GetComponent<Image>().color;
        while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
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
}
