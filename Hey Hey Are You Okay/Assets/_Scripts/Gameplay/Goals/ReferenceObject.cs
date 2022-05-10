using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObject : MonoBehaviour
{
    public SwipeGoal splineObjects;
    public ReferenceObject goal;
    public ReferenceObject mistake;
    public ReferenceObject mistake2;

    public void OnTriggerGoal()
    {
        if (ManageObjects()) splineObjects.correctNum++;

        splineObjects.CheckEndGoal();
    }

    public void OnTriggerMistake()
    {
        if(ManageObjects()) splineObjects.mistakeNum++;

        splineObjects.CheckEndGoal();
    }

    bool ManageObjects()
    {
        if (splineObjects.RemoveCurrentObjectsFromList())
        {
            Destroy(goal.gameObject);
            Destroy(mistake.gameObject);
            Destroy(mistake2.gameObject);

            splineObjects.ActivateNextObjects();
            return true;
        }
        return false;
    }
}
