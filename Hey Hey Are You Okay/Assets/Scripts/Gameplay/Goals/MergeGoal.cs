using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeGoal : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;

    [SerializeField] ToolDrag toolMerger;
    [SerializeField] Texture changeTool;
    [SerializeField] ToolEnum setTool;

    [SerializeField] string eventToPlay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ToolDrag>().tool == goalTool)
        {
            toolMerger.gameObject.GetComponent<RawImage>().texture = changeTool;
            toolMerger.tool = setTool;
            EventManager.Instance.PlayEvent(eventToPlay);
        }
        else if (other.GetComponent<ToolDrag>().tool != goalTool)
        {
            other.GetComponent<ToolDrag>().OnForceEndDrag();
            ExamManager.Instance.ReduceStars();
            Debug.Log("MISTAKE");
        }
    }
}
