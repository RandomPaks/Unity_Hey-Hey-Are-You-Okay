using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndEvent : AEventSequence
{
    [SerializeField] Image bg;
    [SerializeField] Image backButton;

    void Start()
    {
        OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        bg.gameObject.SetActive(true);
        StartCoroutine(FadeInBG());
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
    }
    IEnumerator FadeInBG()
    {
        Color curColor = bg.color;
        while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            bg.color = curColor;
            backButton.color = curColor;
            yield return null;
        }
    }
}
