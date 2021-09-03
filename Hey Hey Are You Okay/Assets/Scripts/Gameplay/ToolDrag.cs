using UnityEngine;
using UnityEngine.EventSystems;

public enum ToolEnum
{
    WATER,
    TOWEL,
    IODINE,
    BANDAID,
    BANDAGE,
    PHONE,
    WET_TOWEL,
    BURN_OINTMENT,
    HAND
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
