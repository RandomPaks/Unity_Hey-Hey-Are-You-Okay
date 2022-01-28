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
    float totalAccuracy, totalProcedures = 1;
    [SerializeField] GameObject endPanel, star1, star2, star3;
    [SerializeField] Text accuracyText;

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
    }

    public void EndExams()
    {
        endPanel.SetActive(true);

        if(totalAccuracy / totalProcedures >= 0.9)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if(totalAccuracy / totalProcedures >= 0.6)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if(totalAccuracy / totalProcedures >= 0.3)
        {
            star1.SetActive(true);
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
            accuracyText.text = "Carefulness: " + (totalAccuracy / totalProcedures * 100).ToString("F2") + "%";
            Debug.Log(totalAccuracy / totalProcedures);
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
            endPanel.GetComponent<Image>().color = curColor;
            accuracyText.color = new Vector4(0.2f, 0.2f, 0.2f, curColor.a);
            foreach (Transform child in endPanel.transform)
            {
                if (child.TryGetComponent<Image>(out Image imageComponent))
                    imageComponent.color = curColor;
            }
            yield return null;
        }
    }
}
