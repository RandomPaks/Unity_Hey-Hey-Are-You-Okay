using UnityEngine;

public enum GoalEnum
{
    BLOOD,
    WATER,
    DRY,
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
                if (toolObject.tool == ToolEnum.FAUCET)
                {
                    GameManager.Instance.isWashing = true;
                }
                break;
            case GoalEnum.WATER:
                if (toolObject.tool == ToolEnum.TOWEL)
                {
                    GameManager.Instance.isDrying = true;
                }
                break;
            case GoalEnum.DRY:
                if (toolObject.tool == ToolEnum.IODINE)
                {
                    GameManager.Instance.isApplying = true;
                }
                break;
            case GoalEnum.IODINE:
                if (toolObject.tool == ToolEnum.BANDAID)
                {
                    GameManager.Instance.isBandAiding = true;
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
                break;
            case GoalEnum.WATER:
                if (toolObject.tool == ToolEnum.TOWEL)
                {
                    GameManager.Instance.isDrying = false;
                }
                break;
            case GoalEnum.DRY:
                if (toolObject.tool == ToolEnum.IODINE)
                {
                    GameManager.Instance.isApplying = false;
                }
                break;
            case GoalEnum.IODINE:
                if (toolObject.tool == ToolEnum.BANDAID)
                {
                    GameManager.Instance.isBandAiding = false;
                }
                break;
        }
    }
}
