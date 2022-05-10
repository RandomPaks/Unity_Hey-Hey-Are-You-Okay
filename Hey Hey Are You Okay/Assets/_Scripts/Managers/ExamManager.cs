using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExamManager : MonoBehaviour
{
    public static ExamManager Instance { get; private set; }

    public string[] scenes;
    string[] savedScenes;
    int procedureCompleted = 0;
    float totalAccuracy = 0, totalProcedures = 3, timer = 0.0f;
    public float totalMistake = 0, totalMoves = 0;
    [SerializeField] GameObject endPanel, perfectPanel, successPanel, failPanel;
    [SerializeField] Image fillStars;
    [SerializeField] Text accuracyText, timeText, mistakesText, movesText;

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
        GameStateManager.IsExam = true;
        StartCoroutine(LateStart(0.1f));
    }

    void Update()
    {
        if(!GameStateManager.IsPaused)
            timer += Time.deltaTime;
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        savedScenes = new string[scenes.Length];
        for(int i = 0; i < scenes.Length; i++)
        {
            savedScenes[i] = string.Copy(scenes[i]);
        }
        StartProcedure();
    }

    public void StartProcedure()
    {
        GameStateManager.IsPaused = false;
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[rand]);
        RemoveElement<String>(ref scenes, rand);
    }

    public void ResetExams()
    {
        scenes = new string[savedScenes.Length];
        for(int i = 0; i < savedScenes.Length; i++)
        {
            scenes[i] = String.Copy(savedScenes[i]);
        }
        timer = 0;
        procedureCompleted = 0;
        totalMoves = 0;
        totalAccuracy = 0;
        totalMistake = 0;

        failPanel.SetActive(false);
        successPanel.SetActive(false);
        perfectPanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void EndExams()
    {
        GameStateManager.IsPaused = true;
        accuracyText.text = "Accuracy: " + (totalAccuracy / totalProcedures * 100).ToString("F2") + "%";
        mistakesText.text = "Mistakes: " + totalMistake;
        movesText.text = "Moves: " + totalMoves;
        timeText.text = timer.ToString("F2") + "s";

        endPanel.SetActive(true);
        fillStars.fillAmount = (totalAccuracy / totalProcedures);

        if (totalAccuracy / totalProcedures >= 0.9)
        {
            perfectPanel.SetActive(true);
        }
        else if(totalAccuracy / totalProcedures >= 0.6)
        {
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }

        StartCoroutine(FadeInEndPanel());
    }

    void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            arr[a] = arr[a + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }

    public void EndProcedure()
    {
        procedureCompleted++;
        totalAccuracy += GameManager.Instance.accuracy;
        if (procedureCompleted == totalProcedures)
        {
            EndExams();
        }
        else
        {
            StartProcedure();
        }
    }

    IEnumerator FadeInEndPanel()
    {
        Color curColor = new Color(1, 1, 1, 0);
        Image[] EndPanelImages = endPanel.GetComponentsInChildren<Image>();
        Text[] EndPanelText = endPanel.GetComponentsInChildren<Text>();

        foreach (Image image in EndPanelImages)
        {
            image.color = curColor;
        }
        foreach (Text text in EndPanelText)
        {
            text.color = new Vector4(0, 0, 0, curColor.a);
        }

        while (Mathf.Abs(curColor.a - 1.0f) > 0.001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            foreach (Image image in EndPanelImages)
            {
                image.color = curColor;
            }
            foreach (Text text in EndPanelText)
            {
                text.color = new Vector4(0, 0, 0, curColor.a);
            }
            yield return null;
        }
    }

    public void OnReplayButton()
    {
        ResetExams();
        StartProcedure();
    }

    public void OnLobbyButton()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
