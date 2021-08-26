using UnityEngine;

public class PhoneGoal : AGoal
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        if (toolObject.tool == ToolEnum.PHONE)
        {
            EventManager.Instance.PlayEvent("Bandage3ToPhone");
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
    }
}
