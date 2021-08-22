
using UnityEngine;

public enum GoalEnum
{
    BLOOD,
    WATER,
    TOWEL,
    IODINE,
    BANDAID
}

public class ToolGoal : MonoBehaviour
{
    public GoalEnum goal;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        switch (goal)
        {
            case GoalEnum.BLOOD:
                if(toolObject.tool == ToolEnum.FAUCET)
                {
                    GameManager.Instance.isWashing = true;
                }
                else
                {

                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        switch (goal)
        {
            case GoalEnum.BLOOD:
                if (toolObject.tool == ToolEnum.FAUCET)
                {
                    GameManager.Instance.isWashing = false;
                }
                else
                {

                }
                break;
        }
    }
}
