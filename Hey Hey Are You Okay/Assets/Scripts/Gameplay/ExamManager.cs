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
    float totalAccuracy = 0, totalProcedures = 1, timer;
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
        PersistentManager.Instance.isExam = true;
        StartCoroutine(LateStart(0.1f));
    }

    void Update()
    {
        if(!PersistentManager.Instance.isPaused)
            timer += Time.deltaTime;
        Debug.Log(timer);
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
        PersistentManager.Instance.isPaused = false;
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[rand]);
        RemoveElement<String>(ref scenes, rand);
    }

    public void RestartExams()
    {
        scenes = new string[savedScenes.Length];
        for(int i = 0; i < savedScenes.Length; i++)
        {
            scenes[i] = String.Copy(savedScenes[i]);
        }
        timer = 0;
    }

    public void EndExams()
    {
        PersistentManager.Instance.isPaused = true;
        accuracyText.text = "Carefulness: " + (totalAccuracy / totalProcedures * 100).ToString("F2") + "%";
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

        StartCoroutine(FadeInBG());
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
