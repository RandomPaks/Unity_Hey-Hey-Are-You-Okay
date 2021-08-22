using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [SerializeField] AEventSequence[] events;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlayEvent(string name)
    {
        AEventSequence a = Array.Find(events, anEvent => anEvent.name == name);
        if (a == null)
        {
            Debug.LogWarning("Event: " + name + " not found!");
            return;
        }

        a.OnPlayEvent();
    }

    public void CancelEvent(string name)
    {
        AEventSequence a = Array.Find(events, anEvent => anEvent.name == name);
        if (a == null)
        {
            Debug.LogWarning("Event: " + name + " not found!");
            return;
        }

        a.OnFinishEvent();
    }
}
