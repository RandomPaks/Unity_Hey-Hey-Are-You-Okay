using UnityEngine;

public class IodineGoal : MonoBehaviour
{
    public GameObject[] goals;
    public GameObject iodine;
    bool isStart = false;
    bool isFinish = false;

    void Update()
    {
        if (goals[0].GetComponent<Collider2D>().IsTouching(iodine.GetComponent<Collider2D>()))
        {
            isStart = true;
            goals[1].SetActive(true);
        }
        if (goals[1].GetComponent<Collider2D>().IsTouching(iodine.GetComponent<Collider2D>()))
        {
            isFinish = true;
            goals[0].SetActive(false);
            goals[1].SetActive(false);
            GameManager.Instance.FinishedSwipeEvent("DryToBandAid");
        }
    }
}
