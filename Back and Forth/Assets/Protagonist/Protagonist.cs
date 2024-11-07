using UnityEngine;

namespace Game
{
	public partial class Protagonist : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private ProtagonistProfile profile;
		#endregion

		#region Properties
		public ProtagonistProfile Profile => profile;
		#endregion

		#region Life cycle
		protected void Start()
		{
			StartControl();
		}
		#endregion
	}
}
