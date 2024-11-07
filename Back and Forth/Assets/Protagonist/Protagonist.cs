using UnityEngine;

namespace Game
{
	public partial class Protagonist : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private ProtagonistProfile profile;
		[SerializeField] private Transform head;
		#endregion

		#region Properties
		public ProtagonistProfile Profile => profile;
		#endregion

		#region Life cycle
		protected void Start()
		{
			StartControl();
		}

		protected void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Passing Trigger")) {
				Game.Instance.OnProtagonistEnterPassingTrigger(other);
			}
		}
		#endregion
	}
}
