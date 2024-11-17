using UnityEngine;

namespace Game
{
	public class Classroom : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private Transform spawnPoint;
		#endregion

		#region Properties
		public Transform SpawnPoint
		{
			get
			{
				if(spawnPoint != null)
					return spawnPoint;
				return transform;
			}
		}
		#endregion

		#region Life cycle
		protected void OnTriggerEnter(Collider other)
		{
			if(other.gameObject == Game.Instance.Protagonist.gameObject)
			{
				Debug.Log($"Player entered {name}.", this);
				Game.Instance.SendMessage("OnPlayerEnterClassroom", this);
			}
		}

		protected void OnTriggerExit(Collider other)
		{
			if(other.gameObject == Game.Instance.Protagonist.gameObject)
			{
				Debug.Log($"Player exited {name}.", this);
				Game.Instance.SendMessage("OnPlayerExitClassroom", this);
			}
		}
		#endregion
	}
}
