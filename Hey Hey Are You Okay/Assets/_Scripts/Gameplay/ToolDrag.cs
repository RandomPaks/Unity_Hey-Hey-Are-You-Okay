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
    HAND,
    BRANCH,
    HANDKERCHIEF,
    ICEPACK,
    ELEVATE,
    REST
}

public class ToolDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public ToolEnum tool;
    PointerEventData lastPointerData;


    Vector3 startPosition;
    Vector3 lastPosition = Vector3.zero;

    private float _toolSpeed;

    public float ToolSpeed => _toolSpeed;

    private void Start()
    {
        startPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        _toolSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        Debug.Log(_toolSpeed);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.Instance.currentTool = this;
        lastPointerData = eventData;

        gameObject.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lastPointerData = null;

        gameObject.transform.position = startPosition;
    }

    public void ForceEndDrag()
    {
        if (lastPointerData != null)
        {
            lastPointerData.pointerDrag = null;

            gameObject.transform.position = startPosition;
        }
    }
}
