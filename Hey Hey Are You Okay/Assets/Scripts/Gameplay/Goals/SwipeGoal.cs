using UnityEngine;

public class SwipeGoal : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;
    [SerializeField] GameObject tool;
    ToolDrag toolObject;
    Collider2D toolCol;
    [SerializeField] GameObject[] goals;
    [SerializeField] string eventToPlay;


    void Awake()
    {
        toolObject = tool.GetComponent<ToolDrag>();
        toolCol = tool.GetComponent<Collider2D>();
    }

    void Update()
    {
        for(int i = 0; i < goals.Length; i++)
        {
            if (goals[i].GetComponent<Collider2D>().IsTouching(toolCol) && toolObject.tool == goalTool)
            {
                goals[i].SetActive(false);
                if (i + 1 < goals.Length)
                {
                    goals[i + 1].SetActive(true);
                }
                else
                {
                    for(int j = 0; j < goals.Length; j++)
                    {
                        goals[j].SetActive(false);
                        GameManager.Instance.FinishedSwipeEvent(eventToPlay);
                    }
                }
            }
        }
    }
}
