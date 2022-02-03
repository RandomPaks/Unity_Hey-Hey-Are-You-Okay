using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Resolves restart button issues
    public void LoadScene(string scene)
    {
        if (PersistentManager.Instance.isExam)
        {
            ExamManager.Instance.RestartExams();
            ExamManager.Instance.StartProcedure();
        }
        else if (PersistentManager.Instance.isSurvival)
        {
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
        if (PersistentManager.Instance.isExam)
        {
            PersistentManager.Instance.isExam = false;
            Destroy(ExamManager.Instance.gameObject);
        }
        else if (PersistentManager.Instance.isSurvival)
        {
            PersistentManager.Instance.isSurvival = false;
            Destroy(SurvivalManager.Instance.gameObject);
        }
    }

    //Survival Manager
    public void Pause()
    {
        PersistentManager.Instance.isPaused = true;
    }

    public void Unpause()
    {
        PersistentManager.Instance.isPaused = false;
    }
}
