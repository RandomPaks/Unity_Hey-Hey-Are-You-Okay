using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageGoal : MonoBehaviour
{
    [SerializeField] GameObject[] goals;
    [SerializeField] GameObject bandage;
    [SerializeField] string nextEventName;


    void Update()
    {
        if (goals[0].GetComponent<Collider2D>().IsTouching(bandage.GetComponent<Collider2D>()))
        {
            goals[1].SetActive(true);
        }
        if (goals[1].GetComponent<Collider2D>().IsTouching(bandage.GetComponent<Collider2D>()))
        {
            goals[2].SetActive(true);
        }
        if (goals[2].GetComponent<Collider2D>().IsTouching(bandage.GetComponent<Collider2D>()))
        {
            goals[0].SetActive(false);
            goals[1].SetActive(false);
            goals[2].SetActive(false);
            GameManager.Instance.FinishedSwipeEvent(nextEventName);
        }
    }
}
