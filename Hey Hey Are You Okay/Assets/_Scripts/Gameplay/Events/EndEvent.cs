using Sound;
using UnityEngine;

public class EndEvent : AEventSequence
{
    [SerializeField] string key;

    void Start()
    {
        AudioManager.Instance.Play("MissionComplete");

        if (ExamManager.Instance != null)
            ExamManager.Instance.EndProcedure();
        else if (SurvivalManager.Instance != null)
            SurvivalManager.Instance.EndProcedure();
        else
            OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        Debug.Log("Total Accuracy: " + GameManager.Instance.accuracy);
        UIManager.Instance.StartEndEvent();
        SaveManager.Instance.PlayerFinishLevel(key);
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
    }
}
