using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
	[SerializeField] private UnityEvent onInteracted;

	[ContextMenu("Interact")]
	public void Interact() {
		if(!isActiveAndEnabled)
			return;

		onInteracted?.Invoke();
	}
}
