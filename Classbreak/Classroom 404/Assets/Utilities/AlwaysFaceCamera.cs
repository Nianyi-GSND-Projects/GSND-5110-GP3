using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour {
	static private Camera mainCam;

	protected void Awake() {
		mainCam = Camera.main;
	}

	protected void LateUpdate() {
		transform.rotation = Quaternion.LookRotation(-mainCam.transform.forward);
		float distance = Vector3.Distance(mainCam.transform.position, transform.position);
		transform.localScale = Mathf.Atan(distance) * Vector3.one;
	}
}