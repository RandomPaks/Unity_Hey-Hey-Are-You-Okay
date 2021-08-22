using UnityEngine;

public class GameEvent : AEventSequence
{
    [SerializeField] GameObject oldHand;
    [SerializeField] GameObject newHand;
    [SerializeField] GameObject oldHitbox;
    [SerializeField] GameObject newHitbox;

    void Start()
    {
        OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        oldHand.SetActive(false);
        newHand.SetActive(true);
        oldHitbox.SetActive(false);
        if(newHitbox != null)
            newHitbox.SetActive(true);
        OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
    }
}
