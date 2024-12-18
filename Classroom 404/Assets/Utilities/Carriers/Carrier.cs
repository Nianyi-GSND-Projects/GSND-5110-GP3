using UnityEngine;

namespace NaniCore {
	public abstract class Carrier : MonoBehaviour {
		#region Serialized fields
		[SerializeField] private Transform target;
		#endregion

		#region Fields
		private new Rigidbody rigidbody;
		#endregion

		#region Interfaces
		public Transform Target {
			get => target;
			set => target = value;
		}
		public Rigidbody Rigidbody => rigidbody;
		#endregion

		#region Life cycle
		protected void Start() {
			if(target == null)
				target = transform;
			rigidbody = Target?.GetComponent<Rigidbody>();
		}
		#endregion
	}
}