using UnityEngine;
using UnityEngine.EventSystems;

public enum ToolEnum
{
    FAUCET,
    TOWEL,
    IODINE,
    BANDAID,
    BANDAGE,
    PHONE
}

public class ToolDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public ToolEnum tool;

    Vector3 startPosition;
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = startPosition;
    }
}
