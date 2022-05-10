using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            other.GetComponent<ReferenceObject>().OnTriggerGoal();
        }
        else if(other.tag == "Mistake")
        {
            other.GetComponent<ReferenceObject>().OnTriggerMistake();
        }
    }
}
