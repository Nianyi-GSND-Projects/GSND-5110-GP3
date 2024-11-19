using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour {
	static private Camera mainCam;

	protected void Awake() {
		mainCam = Camera.main;
	}

	protected void LateUpdate() {
		transform.rotation = Quaternion.LookRotation(-mainCam.transform.forward);
	}
}