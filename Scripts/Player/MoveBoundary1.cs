using UnityEngine;
using System.Collections;

public class MoveBoundary1 : MonoBehaviour {

	public Transform Ninja;

	// Update is called once per frame
	void Update () {
		Vector3 pos = Ninja.position;
		Vector3 boundary = transform.position;
		pos.x = Mathf.Clamp (pos.x, -transform.position.x, transform.position.x);
		pos.z = Mathf.Clamp (pos.z, -transform.position.z, transform.position.z);
		transform.position = pos;
	}
}
