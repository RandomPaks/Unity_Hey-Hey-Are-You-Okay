using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour
{
    public static ExamManager Instance { get; private set; }
    public bool isPlaying = false;
    public bool isTutorial = true;
    public bool isExam = false;
    public String[] scenes;
    String[] savedScenes;

    public int examsPassed = 0;
    public int stars = 3;
    [SerializeField] GameObject resultsPanel, stars3Panel, stars2Panel, stars1Panel, stars0Panel;

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
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPlaying = true;

        savedScenes = new string[scenes.Length];
        for(int i = 0;i < scenes.Length; i++)
        {
            savedScenes[i] = String.Copy(scenes[i]);
        }
    }

    public void StartExams()
    {
        isTutorial = false;
        isExam = true;
        examsPassed = 0;
        stars = 3;

        int rand = UnityEngine.Random.Range(0, scenes.Length);
        Debug.Log("LOADED: " + scenes[rand]);
        SceneManager.LoadScene(scenes[rand]);
        RemoveElement<String>(ref scenes, rand);
    }

    public void ContinueExams()
    {
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        Debug.Log("LOADED: " + scenes[rand]);
        SceneManager.LoadScene(scenes[rand]);
        RemoveElement<String>(ref scenes, rand);
    }

    void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            arr[a] = arr[a + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }

    public void ResetExams()
    {
        isTutorial = true;
        isExam = false;

        scenes = new string[savedScenes.Length];
        for (int i = 0; i < savedScenes.Length; i++)
        {
            scenes[i] = String.Copy(savedScenes[i]);
        }
    }

    public void ShowResults()
    {
        resultsPanel.SetActive(true);

        if(stars == 3)
        {
            stars3Panel.SetActive(true);
        }
        else if(stars == 2)
        {
            stars2Panel.SetActive(true);
        }
        else if(stars == 1)
        {
            stars1Panel.SetActive(true);
        }
        else
        {
            stars0Panel.SetActive(true);
        }
    }

    public void HideResults(float secs)
    {
        StartCoroutine(HideResultsDelay(secs));
    }

    IEnumerator HideResultsDelay(float secs)
    {
        yield return new WaitForSeconds(secs);
        resultsPanel.SetActive(false);
        stars3Panel.SetActive(false);
        stars2Panel.SetActive(false);
        stars1Panel.SetActive(false);
        stars0Panel.SetActive(false);
    }

    public void ReduceStars()
    {
        if (isExam)
        {
            Instance.stars--;
            if (stars <= 0)
            {
                ShowResults();
            }
        }
    }
}
