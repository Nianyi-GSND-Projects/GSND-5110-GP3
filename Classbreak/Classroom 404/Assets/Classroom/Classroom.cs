using UnityEngine;

namespace Game
{
	public class Classroom : MonoBehaviour, IRevertable
	{
		#region Serialized fields
		[SerializeField] private string number = "404";
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
		protected void Start()
		{
			RevertDoorPlateNumber();
		}

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

		#region Interfaces
		public void SetDoorPlateNumber(string number) {
			foreach(var doorplate in transform.GetComponentsInChildren<DoorPlate>()) {
				doorplate.Number = number;
			}
		}

		public void RevertDoorPlateNumber()
		{
			SetDoorPlateNumber(number);
		}

		public void Revert() {
			RevertDoorPlateNumber();
		}
		#endregion
	}
}
