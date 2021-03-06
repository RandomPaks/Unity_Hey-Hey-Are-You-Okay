using System.Collections.Generic;
using UnityEngine;
using Lines;

public class SwipeGoal : MonoBehaviour
{
	public BezierSpline spline;
	public int frequency;
	public Vector3 offsetPosition;
	public Transform goal;
	public Transform mistake;
	public List<Transform> references;
	public float accuracy = 1;
	public int correctNum = 0, mistakeNum = 0;
	[SerializeField] ToolEnum goalTool;
	[SerializeField] string eventToPlay;
	[SerializeField] bool isLastSwipe;

	void Awake()
	{
		if (frequency <= 0 || goal == null || mistake == null)
		{
			return;
		}
		float stepSize = 1f / frequency;
		for (int p = 0; p < frequency; p++)
		{
			Transform item = Instantiate(goal);
			Vector3 position = spline.GetPoint(p * stepSize);
			item.transform.localPosition = position;
			item.transform.parent = transform;
			ReferenceObject referenceObjectGoal = item.gameObject.AddComponent<ReferenceObject>();
			references.Add(item);

			item = Instantiate(mistake);
			item.transform.localPosition = position + offsetPosition;
			item.transform.parent = transform;
			ReferenceObject referenceObjectMistake = item.gameObject.AddComponent<ReferenceObject>();
			references.Add(item);

			item = Instantiate(mistake);
			item.transform.localPosition = position - offsetPosition;
			item.transform.parent = transform;
			ReferenceObject referenceObjectMistake2 = item.gameObject.AddComponent<ReferenceObject>();
			references.Add(item);

			referenceObjectGoal.splineObjects = referenceObjectMistake.splineObjects = referenceObjectMistake2.splineObjects = this;
			referenceObjectGoal.goal = referenceObjectMistake.goal = referenceObjectMistake2.goal = referenceObjectGoal;
			referenceObjectGoal.mistake = referenceObjectMistake.mistake = referenceObjectMistake2.mistake = referenceObjectMistake;
			referenceObjectGoal.mistake2 = referenceObjectMistake.mistake2 = referenceObjectMistake2.mistake2 = referenceObjectMistake2;

			if (p > 0)
			{
				referenceObjectGoal.gameObject.SetActive(false);
				referenceObjectMistake.gameObject.SetActive(false);
				referenceObjectMistake2.gameObject.SetActive(false);
			}
		}
	}

	bool isCorrectTool => GameManager.Instance.currentTool.tool == goalTool;
	bool hasReferenceObjects => references.Count > 0;
	bool isRemoveable => isCorrectTool && hasReferenceObjects;
	public bool RemoveCurrentObjectsFromList()
	{
		if (GameManager.Instance.currentTool != null && isRemoveable)
		{
			references.RemoveAt(0);
			references.RemoveAt(0);
			references.RemoveAt(0);
			return true;
		}
		else if (GameManager.Instance.currentTool != null && !isCorrectTool)
        {
			GameManager.Instance.MakeMistake();
			return false;
		}
		return false;
	}

	bool emptyReferenceObjects => references.Count == 0;
	public void ActivateNextObjects()
	{
		if (hasReferenceObjects)
		{
			references[0].gameObject.SetActive(true);
			references[1].gameObject.SetActive(true);
			references[2].gameObject.SetActive(true);
		}
	}

	public void CheckEndGoal()
    {
		if (emptyReferenceObjects)
		{
			accuracy = (float)correctNum / (float)frequency * 1;

			GameManager.Instance.FinishSwipeEvent(eventToPlay, accuracy, isLastSwipe);
		}
	}
}
