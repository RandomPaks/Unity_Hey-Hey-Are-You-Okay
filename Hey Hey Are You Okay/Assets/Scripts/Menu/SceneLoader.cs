using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Resolves restart button issues
    public void LoadScene(string scene)
    {
        if (ExamManager.Instance != null)
        {
            ExamManager.Instance.RestartExams();
            ExamManager.Instance.StartProcedure();
        }
        else if (SurvivalManager.Instance != null)
        {
            SurvivalManager.Instance.RestartSurvival();
            SurvivalManager.Instance.StartProcedure();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        if (ExamManager.Instance != null)
        {
            Destroy(ExamManager.Instance.gameObject);
        }
        else if (SurvivalManager.Instance != null)
        {
            Destroy(SurvivalManager.Instance.gameObject);
        }
    }
}
