using UnityEngine;

public class SwipeGoal : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;
    [SerializeField] GameObject tool;
    ToolDrag toolDragger;
    Collider2D toolCol;
    [SerializeField] GameObject[] goals;
    [SerializeField] string eventToPlay;
    [SerializeField] bool isLast = false;

    void Awake()
    {
        toolDragger = tool.GetComponent<ToolDrag>();
        toolCol = tool.GetComponent<Collider2D>();
    }

    void Update()
    {
        for (int i = 0; i < goals.Length; i++)
        {
            if (goals[i].GetComponent<Collider2D>().IsTouching(toolCol) && toolDragger.tool == goalTool)
            {
                goals[i].SetActive(false);
                if (i + 1 < goals.Length)
                {
                    goals[i + 1].SetActive(true);
                }
                else
                {
                    if (isLast)
                    {
                        toolDragger.OnForceEndDrag();
                    }

                    goals[i].SetActive(false);
                    GameManager.Instance.FinishSwipeEvent(eventToPlay, 0);
                }
            }
        }
    }
}
