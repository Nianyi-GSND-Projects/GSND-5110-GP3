using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(Protagonist))]
	public class ProtagonistInput : MonoBehaviour
	{
		private Protagonist protagonist;
		private PlayerInput playerInput;

		#region Life cycle
		protected void Start()
		{
			protagonist = GetComponent<Protagonist>();

			playerInput = GetComponent<PlayerInput>();
			playerInput.actions = Instantiate(playerInput.actions);

			MovementEnabled = true;
			OrientationEnabled = true;
		}

		protected void FixedUpdate()
		{
			FixedUpdateMovement();
		}

		protected void OnEnable()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		protected void OnDisable()
		{
			Cursor.lockState = CursorLockMode.None;
		}
		#endregion

		#region Functions
		private bool GetActionMapEnabled(string name)
		{
			return playerInput.actions.FindActionMap(name).enabled;
		}

		private void SetActionMapEnabled(string name, bool enabled)
		{
			var map = playerInput.actions.FindActionMap(name);
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
		public bool OrientationEnabled {
			get => GetActionMapEnabled("Orientation");
			set => SetActionMapEnabled("Orientation", value);
		}

		protected void OnOrientDelta(InputValue value) {
			var raw = value.Get<Vector2>();
			Vector2 delta = raw * protagonist.Profile.baseOrientationSpeed;
			if(protagonist.Profile.invertY)
				delta.y = -delta.y;
			protagonist.RotateDelta(delta);
		}
		#endregion
	}
}
