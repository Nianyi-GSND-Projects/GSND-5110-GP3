using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public partial class Protagonist
	{
		#region Fields
		private new CapsuleCollider collider;
		private new Rigidbody rigidbody;
		private ProtagonistInput input;
		#endregion

		#region Life cycle
		private void StartControl()
		{
			collider = GetComponent<CapsuleCollider>();
			rigidbody = GetComponent<Rigidbody>();
			input = GetComponent<ProtagonistInput>();
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

		/**
		 * <param name="velocity">
		 * World-space velocity.
		 * </param>
		 */
		public void MoveVelocity(Vector3 velocity)
		{
			rigidbody.velocity = velocity;
		}

		/**
		 * <param name="delta">
		 * The angle to rotate relative to current direction, in degrees.
		 * </param>
		 */
		public void RotateDelta(Vector2 delta)
		{
			head.Rotate(Vector3.right, delta.y, Space.Self);
			transform.Rotate(Vector3.up, delta.x, Space.Self);
		}

		public void Jump(float height)
		{
			if(height <= 0.0f)
				return;

			float speed = Mathf.Sqrt(2.0f * height * Physics.gravity.magnitude);
			rigidbody.AddForce(Physics.gravity.normalized * -speed, ForceMode.VelocityChange);
		}
		#endregion
	}
}
