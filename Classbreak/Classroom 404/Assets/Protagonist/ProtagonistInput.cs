using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

namespace Game
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(Protagonist))]
	public class ProtagonistInput : MonoBehaviour
	{
		#region Fields
		private Protagonist protagonist;
		private PlayerInput playerInput;

		private PlayerInput PlayerInput
		{
			get
			{
				if(playerInput == null)
					playerInput = GetComponent<PlayerInput>();
				return playerInput;
			}
		}
		#endregion

		#region Life cycle
		protected void Start()
		{
			protagonist = GetComponent<Protagonist>();

			PlayerInput.actions = Instantiate(PlayerInput.actions);

			MovementEnabled = true;
			OrientationEnabled = true;
			InteractionEnabled = true;
		}

		protected void FixedUpdate()
		{
			FixedUpdateMovement();
		}

		protected void OnEnable()
		{
			PlayerInput.enabled = true;
		}

		protected void OnDisable()
		{
			PlayerInput.enabled = false;
		}
		#endregion

		#region Functions
		private bool GetActionMapEnabled(string name)
		{
			return PlayerInput.actions.FindActionMap(name).enabled;
		}

		private void SetActionMapEnabled(string name, bool enabled)
		{
			var map = PlayerInput.actions.FindActionMap(name);
			if(enabled)
				map.Enable();
			else
				map.Disable();
		}
		#endregion

		#region Movement
		private Vector3 localMovementVelocity = default;

		public bool MovementEnabled
		{
			get => GetActionMapEnabled("Movement");
			set => SetActionMapEnabled("Movement", value);
		}

		private void FixedUpdateMovement()
		{
			Vector3 worldMovementVelocity = protagonist.transform.localToWorldMatrix.MultiplyVector(localMovementVelocity);
			protagonist.MoveVelocity(worldMovementVelocity);
		}

		protected void OnMoveVelocity(InputValue value)
		{
			var raw = value.Get<Vector2>();
			localMovementVelocity = new Vector3(raw.x, 0, raw.y) * protagonist.Profile.baseMovementSpeed;
		}
		#endregion

		#region Orientation
		public bool OrientationEnabled
		{
			get => GetActionMapEnabled("Orientation");
			set => SetActionMapEnabled("Orientation", value);
		}

		protected void OnOrientDelta(InputValue value)
		{
			var raw = value.Get<Vector2>();
			Vector2 delta = raw * protagonist.Profile.baseOrientationSpeed;
			if(protagonist.Profile.invertY)
				delta.y = -delta.y;
			protagonist.RotateDelta(delta);
		}
		#endregion

		#region Interaction
		public bool InteractionEnabled
		{
			get => GetActionMapEnabled("Interaction");
			set => SetActionMapEnabled("Interaction", value);
		}

		protected void OnInteract()
		{
			protagonist.Interact();
		}
		#endregion
	}
}
