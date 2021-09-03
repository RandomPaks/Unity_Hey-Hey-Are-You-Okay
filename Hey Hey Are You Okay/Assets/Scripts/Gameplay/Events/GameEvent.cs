using UnityEngine;

public class GameEvent : AEventSequence
{
    [SerializeField] GameObject oldHand;
    [SerializeField] GameObject newHand;
    [SerializeField] GameObject oldHitbox;
    [SerializeField] GameObject newHitbox;
    [SerializeField] GameObject cancelTextEvent;
    [SerializeField] string soundEvent;

    void Start()
    {
        OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        if(oldHand != null)
        {
            oldHand.SetActive(false);
        }
        if (newHand != null)
        {
            newHand.SetActive(true);
        }
        if (oldHitbox != null)
            oldHitbox.SetActive(false);
        if(newHitbox != null)
            newHitbox.SetActive(true);
        OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        if(cancelTextEvent != null)
            cancelTextEvent.SetActive(false);
        AudioManager.Instance.Play(soundEvent);
        base.OnFinishEvent();
    }
}
