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
    [SerializeField] Text textBox;
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] GameObject textBG;

    void Start()
    {
        if (ExamManager.Instance == null && SurvivalManager.Instance == null)
            OnPlayEvent();
        else
            OnFinishEvent();
    }

    public override void OnPlayEvent()
    {
        textBG.SetActive(true);
        StartCoroutine(SetText());
    }

    public override void OnFinishEvent()
    {
        textBG.SetActive(false);
        base.OnFinishEvent();
    }

    IEnumerator SetText()
    {
        foreach(Dialogue dialogue in dialogues)
        {
            textBox.text = dialogue.text;
            yield return new WaitForSeconds(dialogue.secs);
        }
        textBox.text = "";
        OnFinishEvent();
    }
}
