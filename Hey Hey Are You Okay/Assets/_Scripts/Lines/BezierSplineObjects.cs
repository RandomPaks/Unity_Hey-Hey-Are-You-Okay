using System.Collections.Generic;
using UnityEngine;

namespace Lines
{
	public class BezierSplineObjects : MonoBehaviour
	{
		public BezierSpline spline;
		public int frequency;
		public Vector3 offsetPosition;
		public Transform goal;
		public Transform mistake;
		public List<Transform> references;
		public float accuracy = 1;
		public int correctNum = 0, mistakeNum = 0;

		void Awake()
		{
			if (frequency <= 0 || goal == null || mistake == null)
			{
				return;
			}

			float stepSize = 1f / frequency;
			for (int p = 0; p < frequency; p++)
			{
				Transform item = Instantiate(goal) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.transform.localPosition = position;
				item.transform.parent = transform;
				ReferenceObject referenceObjectGoal = item.gameObject.AddComponent<ReferenceObject>();
				references.Add(item);

				item = Instantiate(mistake) as Transform;
				item.transform.localPosition = position + offsetPosition;
				item.transform.parent = transform;
				ReferenceObject referenceObjectMistake = item.gameObject.AddComponent<ReferenceObject>();
				references.Add(item);

				item = Instantiate(mistake) as Transform;
				item.transform.localPosition = position - offsetPosition;
				item.transform.parent = transform;
				ReferenceObject referenceObjectMistake2 = item.gameObject.AddComponent<ReferenceObject>();
				references.Add(item);

				//referenceObjectGoal.splineObjects = referenceObjectMistake.splineObjects = referenceObjectMistake2.splineObjects = this;
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

		public void RemoveCurrentObjects()
		{
			if (references.Count > 0)
			{
				references.RemoveAt(0);
				references.RemoveAt(0);
				references.RemoveAt(0);
			}
			accuracy = (float)correctNum / (float)frequency * 1;
			Debug.Log(accuracy);
		}

		public void ActivateNextObjects()
		{
			if (references.Count > 0)
			{
				references[0].gameObject.SetActive(true);
				references[1].gameObject.SetActive(true);
				references[2].gameObject.SetActive(true);
			}
			else if (references.Count == 0)
			{
				Debug.Log("Finished!");
			}
		}
	}

}