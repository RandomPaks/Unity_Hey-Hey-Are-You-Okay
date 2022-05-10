using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Image lobbyButtonImage;
    [SerializeField] Sprite lobbyButtonSprite;

    void Start()
    {
        if (PersistentManager.Instance.isExam || PersistentManager.Instance.isSurvival)
        {
            lobbyButtonImage.sprite = lobbyButtonSprite;
        }
    }

    //Resolves restart button issues
    public void LoadScene(string scene)
    {
        if (PersistentManager.Instance.isExam)
        {
            ExamManager.Instance.ResetExams();
            ExamManager.Instance.StartProcedure();
        }
        else if (PersistentManager.Instance.isSurvival)
        {
            SurvivalManager.Instance.ResetSurvival();
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
            Destroy(ExamManager.Instance.gameObject);
        }
        else if (PersistentManager.Instance.isSurvival)
        {
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
