using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndEvent : AEventSequence
{
    [SerializeField] GameObject endPanel;
    [SerializeField] string key;

    void Start()
    {
        if (ExamManager.Instance != null)
            ExamManager.Instance.EndProcedure();
        else if (SurvivalManager.Instance != null)
            SurvivalManager.Instance.EndProcedure();
        else
            OnPlayEvent();
    }

    public override void OnPlayEvent()
    {
        StartCoroutine(FadeInBG());
        endPanel.SetActive(true);
        Debug.Log("Total Accuracy: " + GameManager.Instance.accuracy);
        SaveManager.Instance.PlayerFinishLevel(key);
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
    }

    IEnumerator FadeInBG()
    {
        Color curColor = endPanel.GetComponent<Image>().color;
        while (Mathf.Abs(curColor.a - 1.0f) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1.0f, 1.5f * Time.deltaTime);
            foreach (Transform child in endPanel.transform)
            {
                if (child.TryGetComponent(out Image imageComponent))
                    imageComponent.color = curColor;
            }
            yield return null;
        }
    }
}
