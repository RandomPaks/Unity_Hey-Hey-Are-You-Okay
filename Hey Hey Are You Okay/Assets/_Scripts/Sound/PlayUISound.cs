using UnityEngine;
using UnityEngine.EventSystems;

namespace Sound
{
    public class PlayUISound : MonoBehaviour
    {
        [SerializeField] string onClickSound;

        void Start()
        {
            EventTrigger trigger;
            if (!TryGetComponent<EventTrigger>(out trigger))
            {
                trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;
            }

            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerClick;
            enter.callback.AddListener((eventData) => { OnClick(); });
            trigger.triggers.Add(enter);
        }

        void OnClick()
        {
            AudioManager.Instance.Play(onClickSound);
        }
    }
}