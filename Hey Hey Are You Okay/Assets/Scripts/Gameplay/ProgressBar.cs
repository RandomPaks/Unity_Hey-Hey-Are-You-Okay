using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    public void AddCurrentFill(float progress)
    {
        current += progress;
    }

    public void EmptyCurrentFill()
    {
        current = 0;
    }

    public void SetCurrentFill(float progress)
    {
        current = progress;
    }
}
