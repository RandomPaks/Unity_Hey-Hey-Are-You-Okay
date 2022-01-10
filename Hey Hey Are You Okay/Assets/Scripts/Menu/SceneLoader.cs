using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Resolves restart button issues
    public void LoadScene(string scene)
    {
        if(ExamManager.Instance != null)
        {
            ExamManager.Instance.RestartExams();
            ExamManager.Instance.StartProcedure();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void LoadMainMenu()
    {
        if (ExamManager.Instance != null)
        {
            Destroy(ExamManager.Instance.gameObject);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
