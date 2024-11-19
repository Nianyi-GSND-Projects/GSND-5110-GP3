using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(Animator))]
	public partial class Protagonist
	{
		#region Fields
		private CharacterController controller;
		private ProtagonistInput input;
		private Animator animator;

		private bool hasReceivedInput = false;
		#endregion

		#region Life cycle
		private void StartControl()
		{
			controller = GetComponent<CharacterController>();
			input = GetComponent<ProtagonistInput>();
			animator = GetComponent<Animator>();
		}

		private void FixedUpdateControl() {
			animator.SetBool("IsMoving", hasReceivedInput);
			hasReceivedInput = false;
		}
		#endregion

		#region Interface
		public bool EnableControl
		{
			get
			{
				if(!input)
					return false;
				return input.enabled;
			}
			set
			{
				if(!input && value)
				{
					Debug.LogWarning("Cannot enable input due to missing ProtagonistInput component.");
					return;
				}
				input.enabled = value;
			}
		}

		public void AlignTo(Transform target) {
			controller.enabled = false;
			transform.position = target.position;
			transform.rotation = Quaternion.LookRotation(target.forward, -Physics.gravity);
			controller.enabled = true;
		}

		/**
		 * <param name="velocity">
		 * World-space velocity.
		 * </param>
		 */
		public void MoveVelocity(Vector3 velocity)
		{
			hasReceivedInput = velocity.magnitude > Mathf.Epsilon;
			controller.SimpleMove(velocity);
		}

		/**
		 * <param name="delta">
		 * The angle to rotate relative to current direction, in degrees.
		 * </param>
		 */
		public void RotateDelta(Vector2 delta)
		{
			var bodyAngles = transform.localEulerAngles;
			bodyAngles.y += delta.x;
			transform.localEulerAngles = bodyAngles;

			var headAngles = head.localEulerAngles;
			float clamp = Profile.zenithClamp;
			if(headAngles.x < 180.0f)
				headAngles.x = Mathf.Min(headAngles.x + delta.y, clamp);
			else
				headAngles.x = Mathf.Max(headAngles.x + delta.y, 360.0f - clamp);
			head.localEulerAngles = headAngles;
		}
		#endregion
	}
}
