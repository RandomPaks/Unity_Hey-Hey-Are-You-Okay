using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGoal2 : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;
    [SerializeField] string eventToPlay;
    [SerializeField] float progressSpeed = 0.5f;

    public bool isProgressing;

    void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.currentTool != null && GameManager.Instance.currentTool.tool == goalTool)
        {
            GameManager.Instance.Progress(eventToPlay, progressSpeed);
        }
    }
}
