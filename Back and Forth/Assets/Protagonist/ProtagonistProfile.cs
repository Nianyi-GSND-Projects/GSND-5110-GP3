using UnityEngine;

namespace Game {
	[CreateAssetMenu(menuName = "Game/Protagonist Profile")]
	public class ProtagonistProfile : ScriptableObject {
		[Header("Control")]
		[Min(0)] public float baseMovementSpeed = 1.0f;
		[Min(0)] public float baseOrientationSpeed = 1.0f;
		public bool invertY = true;
	}
}