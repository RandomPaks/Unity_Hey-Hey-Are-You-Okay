using UnityEngine;

public class SwipeGoalMistake : MonoBehaviour
{
    public ToolEnum goalTool;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);

        if (toolObject.tool != goalTool)
        {
            toolObject.GetComponent<ToolDrag>().OnForceEndDrag();
            ExamManager.Instance.ReduceStars();
        }
    }
}
