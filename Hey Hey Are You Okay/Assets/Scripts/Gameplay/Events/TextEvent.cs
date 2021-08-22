﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string text;
    public float secs;
}

public class TextEvent : AEventSequence
{
    [SerializeField] Text textBox;
    [SerializeField] Dialogue[] dialogues;

    void Start()
    {
        OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        StartCoroutine(SetText());
    }

    public override void OnFinishEvent()
    {
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