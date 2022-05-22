using UnityEngine;

public abstract class AEventSequence : MonoBehaviour
{
    public new string name;

    [SerializeField] protected AEventSequence nextEvent;

    public abstract void OnPlayEvent();

    public virtual void OnFinishEvent()
    {
        if (nextEvent != null)
        {
            nextEvent.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
