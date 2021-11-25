using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObject : MonoBehaviour
{
    public BezierSplineObjects splineObjects;
    public ReferenceObject goal;
    public ReferenceObject mistake;
    public ReferenceObject mistake2;

    public void OnTriggerGoal()
    {
        splineObjects.correctNum++;
        splineObjects.RemoveObjects();

        Destroy(goal.gameObject);
        Destroy(mistake.gameObject);
        Destroy(mistake2.gameObject);

        splineObjects.ActivateObjects();
    }

    public void OnTriggerMistake()
    {
        splineObjects.RemoveObjects();

        Destroy(goal.gameObject);
        Destroy(mistake.gameObject);
        Destroy(mistake2.gameObject);

        splineObjects.ActivateObjects();
    }
}
