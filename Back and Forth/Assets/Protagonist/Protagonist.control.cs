using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(CharacterController))]
	public partial class Protagonist
	{
		#region Fields
		private CharacterController cc;
		#endregion

		#region Life cycle
		private void StartControl()
		{
			cc = GetComponent<CharacterController>();
		}
		#endregion

		#region Interface
		public bool EnableControl
		{
			get
			{
				if(!TryGetComponent<ProtagonistInput>(out var input))
					return false;
				return input.enabled;
			}
			set
			{
				if(!TryGetComponent<ProtagonistInput>(out var input))
				{
					if(value)
					{
						Debug.LogWarning("Cannot enable input due to missing ProtagonistInput component.");
						return;
					}
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
			cc.SimpleMove(velocity);
		}

		public void Jump(float height)
		{
			if(height <= 0.0f)
				return;
			float speed = Mathf.Sqrt(2.0f * height * Physics.gravity.magnitude);
			// TODO
		}
		#endregion
	}
}
