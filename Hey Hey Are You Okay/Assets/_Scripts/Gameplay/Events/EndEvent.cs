using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndEvent : AEventSequence
{
    [SerializeField] string key;

    void Start()
    {
        if (ExamManager.Instance != null)
            ExamManager.Instance.EndProcedure();
        else if (SurvivalManager.Instance != null)
            SurvivalManager.Instance.EndProcedure();
        else
            OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        UIManager.Instance.StartEndEvent();
        Debug.Log("Total Accuracy: " + GameManager.Instance.accuracy);
        SaveManager.Instance.PlayerFinishLevel(key);
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
    }
}
