using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class Classroom : MonoBehaviour, IRevertable
	{
		#region Serialized fields
		[SerializeField] private string number = "404";
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private Text mapLabel;
		[SerializeField] private Text[] doorPlates;
		[SerializeField] private Text[] exitBoards;
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
		public IEnumerable<Text> DoorPlates => doorPlates.Concat(exitBoards);
		#endregion

		#region Life cycle
		protected void Start()
		{
			RevertDoorPlateNumber();
			foreach(var board in exitBoards)
				board.text = number;
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
		public void SetDoorPlateNumber(string number)
		{
			foreach(var plate in doorPlates)
				plate.text = number;
			foreach(var board in exitBoards)
				board.text = number;
			if(mapLabel != null)
				mapLabel.text = number;
		}

		public void RevertDoorPlateNumber()
		{
			SetDoorPlateNumber(number);
		}

		public void Revert()
		{
			RevertDoorPlateNumber();
			LightsOn = true;
		}

		public bool LightsOn
		{
			set
			{
				if(!TryGetComponent<LightGroup>(out var g))
					return;

				if(value)
					g.TurnOn();
				else
					g.TurnOff();
			}
		}
		#endregion
	}
}
