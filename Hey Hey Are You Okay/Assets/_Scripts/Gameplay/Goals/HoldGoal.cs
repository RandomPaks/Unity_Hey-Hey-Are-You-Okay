using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGoal : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;
    [SerializeField] string eventToPlay;
    [SerializeField] float progressSpeed = 0.5f;
    [SerializeField] bool isGoalRub = false;

    void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.currentTool != null && GameManager.Instance.currentTool.tool == goalTool)
        {
            if (isGoalRub)
            {
                if (GameManager.Instance.currentTool.ToolSpeed > 0.1f)
                {
                    GameManager.Instance.Progress(eventToPlay, progressSpeed);
                }
            }
            else
            {
                GameManager.Instance.Progress(eventToPlay, progressSpeed);
            }
        }
        else if (GameManager.Instance.currentTool != null && GameManager.Instance.currentTool.tool != goalTool)
        {
            GameManager.Instance.MakeMistake();
        }
    }
}
