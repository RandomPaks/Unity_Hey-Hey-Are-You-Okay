using UnityEngine;

public class SwipeGoal : MonoBehaviour
{
    [SerializeField] GameObject tool;
    [SerializeField] GameObject[] goals;
    [SerializeField] string eventToPlay;

    void Update()
    {
        if (goals[0].GetComponent<Collider2D>().IsTouching(tool.GetComponent<Collider2D>()))
        {
            goals[1].SetActive(true);
        }
        if (goals[1].GetComponent<Collider2D>().IsTouching(tool.GetComponent<Collider2D>()))
        {
            goals[0].SetActive(false);
            goals[1].SetActive(false);
            GameManager.Instance.FinishedSwipeEvent(eventToPlay);
        }
    }
}
