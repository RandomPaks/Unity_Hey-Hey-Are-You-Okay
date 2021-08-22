using UnityEngine;

public class WaterGoal : MonoBehaviour
{
    public GameObject[] goals;
    public GameObject towel;

    void Update()
    {
        if (goals[0].GetComponent<Collider2D>().IsTouching(towel.GetComponent<Collider2D>()))
        {
            goals[1].SetActive(true);
        }
        if (goals[1].GetComponent<Collider2D>().IsTouching(towel.GetComponent<Collider2D>()))
        {
            goals[0].SetActive(false);
            goals[1].SetActive(false);
            GameManager.Instance.FinishedSwipeEvent("WaterToDry");
        }
    }
}
