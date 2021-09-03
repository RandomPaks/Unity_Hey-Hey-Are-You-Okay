using UnityEngine;

public class HoldGoal : MonoBehaviour
{
    [SerializeField] ToolEnum goalTool;
    [SerializeField] string eventToPlay;
    [SerializeField] float progressSpeed= 0.5f;
    [SerializeField] bool isInstant = false;

    RectTransform rect;
    CircleCollider2D col;
    [Header("Size Decrease")]
    [SerializeField] bool isDecreaseSize = false;
    [Range(2, 5)]
    [SerializeField] float decreaseMult = 2;
    ToolDrag toolCheck;

    public bool isProgressing;

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

        if (isProgressing)
        {
            GameManager.Instance.Progress(eventToPlay, toolCheck, progressSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);

        if (toolObject.tool == goalTool)
        {
            if (!isInstant)
            {
                isProgressing = true;
                toolCheck = toolObject;
            }
            else
            {
                toolObject.OnForceEndDrag();
                EventManager.Instance.PlayEvent(eventToPlay);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<ToolDrag>(out ToolDrag toolObject);

        if (toolObject.tool == goalTool)
        {
            isProgressing = false;
        }
    }
}
