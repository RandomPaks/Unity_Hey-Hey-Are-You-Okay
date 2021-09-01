using UnityEngine;

public class BandAidGoal : AGoal
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
