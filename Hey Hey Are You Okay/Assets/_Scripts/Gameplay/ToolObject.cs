using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            other.GetComponent<ReferenceObject>().OnTriggerGoal();
        }
        else if(other.CompareTag("Mistake"))
        {
            other.GetComponent<ReferenceObject>().OnTriggerMistake();
        }
    }
}
