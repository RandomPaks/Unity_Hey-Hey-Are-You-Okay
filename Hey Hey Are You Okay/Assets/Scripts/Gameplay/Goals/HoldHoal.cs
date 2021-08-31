using UnityEngine;

public class HoldHoal : AGoal
{
    RectTransform rect;
    CircleCollider2D col;
    [SerializeField] float decreaseMult = 2;
    [SerializeField] bool isDecreaseSize = false;

    void Awake()
    {
        if (isDecreaseSize)
        {
            rect = GetComponent<RectTransform>();
            col = GetComponent<CircleCollider2D>();
        }
    }

    void Update()
    {
        if (isDecreaseSize)
        {
            float newScale = ((100 * decreaseMult) - GameManager.Instance.progress) / (100 * decreaseMult);
            rect.localScale = new Vector3(newScale, newScale, 0);
            col.radius = ((50 * decreaseMult) - GameManager.Instance.progress) / decreaseMult;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        if (toolObject.tool == ToolEnum.FAUCET)
        {
            GameManager.Instance.isWashing = true;
        }
        else if(toolObject.tool == ToolEnum.TOWEL)
        {
            GameManager.Instance.isDrying = true;
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);
        if (toolObject.tool == ToolEnum.FAUCET)
        {
            GameManager.Instance.isWashing = false;
        }
        else if (toolObject.tool == ToolEnum.TOWEL)
        {
            GameManager.Instance.isDrying = false;
        }
    }
}
