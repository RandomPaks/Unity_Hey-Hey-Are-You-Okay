using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour
{
    public static ExamManager Instance { get; private set; }

    public string[] scenes;
    string[] savedScenes;

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

        savedScenes = new string[scenes.Length];
        for(int i = 0; i < scenes.Length; i++)
        {
            savedScenes[i] = string.Copy(scenes[i]);
        }
        StartExams();
    }

    void StartExams()
    {
        int rand = UnityEngine.Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[rand]);
        RemoveElement<String>(ref scenes, rand);
    }

    public void EndExams()
    {

    }
    void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            arr[a] = arr[a + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }
}
