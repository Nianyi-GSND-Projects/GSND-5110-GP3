using UnityEngine;

namespace Game
{
	public partial class Protagonist
	{
		[Header("Interaction")]
		[SerializeField] private Transform interactionIndicator;
		private Interactable focused;
		private Vector3 focusPoint;

		public void Interact()
		{
			if(focused != null)
				focused.Interact();
		}

		private void UpdateInteraction()
		{
			UpdateFocus();
			if(focused != null) {
				interactionIndicator.gameObject.SetActive(true);
				interactionIndicator.position = focusPoint;
			}
			else {
				interactionIndicator.gameObject.SetActive(false);
			}
		}

		private void UpdateFocus()
		{
			focused = null;
			Ray ray = new(eye.position, eye.forward);
			if(!Physics.Raycast(ray, out RaycastHit hit, Profile.interactionDistance, ~0, QueryTriggerInteraction.Ignore))
				return;
			var target = hit.collider.transform;
			if(!target.TryGetComponent<Interactable>(out var interactable))
				return;
			focused = interactable;
			focusPoint = hit.point;
		}
	}
}