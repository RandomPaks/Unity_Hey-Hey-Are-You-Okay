using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{   
    [TextArea]
    public string text;
    public float secs;
}

public class TextEvent : AEventSequence
{
    [SerializeField] Dialogue[] dialogues;

    void Start()
    {
        if (ExamManager.Instance == null && SurvivalManager.Instance == null)
            OnPlayEvent();
        else
            OnFinishEvent();
    }

    public override void OnPlayEvent()
    {
        UIManager.Instance.TextEventBGObject.SetActive(true);
        StartCoroutine(SetText());
    }

    public override void OnFinishEvent()
    {
        UIManager.Instance.TextEventBGObject.SetActive(false);
        base.OnFinishEvent();
    }

    IEnumerator SetText()
    {
        foreach(Dialogue dialogue in dialogues)
        {
            UIManager.Instance.TextEventText.text = dialogue.text;
            yield return new WaitForSeconds(dialogue.secs);
        }
        UIManager.Instance.TextEventText.text = "";
        OnFinishEvent();
    }
}
