using UnityEngine;
using System.Collections;

public class MoveBoundary : MonoBehaviour {

	public Transform Ninja;
	public float sphereRadius;

	// Update is called once per frame
	void Update () {
		Vector3 pos = Ninja.position;
		float angle = Mathf.Atan2 (pos.z, pos.x);
		float distance = Mathf.Clamp (pos.magnitude, 0.0f, sphereRadius);
		pos.x = Mathf.Cos (angle) * distance;
		pos.z = Mathf.Sin (angle) * distance;
		Ninja.position = pos;
		/*
		Vector3 pos = Ninja.position;
		pos.x = Mathf.Clamp (pos.x, -8, 8);
		pos.z = Mathf.Clamp (pos.z, -18, 7);
		transform.position = pos;
		*/
	}
}
