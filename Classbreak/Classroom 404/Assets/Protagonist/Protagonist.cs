using UnityEngine;

namespace Game
{
	public partial class Protagonist : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private ProtagonistProfile profile;
		[SerializeField] private Transform head, eye;
		#endregion

		#region Properties
		public ProtagonistProfile Profile => profile;
		#endregion

		#region Life cycle
		protected void Start()
		{
			StartControl();
		}

		protected void Update()
		{
			UpdateInteraction();
		}

		protected void FixedUpdate()
		{
			FixedUpdateControl();
		}
		#endregion
	}
}
