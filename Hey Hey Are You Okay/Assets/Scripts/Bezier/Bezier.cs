using UnityEngine;

public static class Bezier {

	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		t = Mathf.Clamp01(t);
		float x = 1f - t;
		return
			x * x * p0 +
			2f * x * t * p1 +
			t * t * p2;
	}

	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float x = 1f - t;
		return
			x * x * x * p0 +
			3f * x * x * t * p1 +
			3f * x * t * t * p2 +
			t * t * t * p3;
	}

	public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float x = 1f - t;
		return
			3f * x * x * (p1 - p0) +
			6f * x * t * (p2 - p1) +
			3f * t * t * (p3 - p2);
	}
}