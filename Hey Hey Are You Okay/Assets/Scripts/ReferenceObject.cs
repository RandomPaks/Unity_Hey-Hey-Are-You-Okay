using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObject : MonoBehaviour
{
    public SwipeGoal2 splineObjects;
    public ReferenceObject goal;
    public ReferenceObject mistake;
    public ReferenceObject mistake2;

    public void OnTriggerGoal()
    {
        if (ManageObjects()) splineObjects.correctNum++;
    }

    public void OnTriggerMistake()
    {
        if(ManageObjects()) splineObjects.mistakeNum++;
    }

    bool ManageObjects()
    {
       if(splineObjects.RemoveCurrentObjects())
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
