﻿using UnityEngine;

public class BloodGoal : AGoal
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        if (toolObject.tool == ToolEnum.FAUCET)
        {
            GameManager.Instance.isWashing = true;
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        if (toolObject.tool == ToolEnum.FAUCET)
        {
            GameManager.Instance.isWashing = false;
        }
    }
}
