using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Level")]
		[SerializeField] private Collider dormitoryRange;
		[SerializeField] private Collider warehouseRange;
		[SerializeField] private Transform dormitorySpawn, warehouseSpawn;
		[SerializeField] private Level[] levels;
		[SerializeField] private int levelIndex = 0;
		#endregion

		#region Fields
		private Level currentLevel = null;
		private bool boxPickedUp = false;
		#endregion

		#region Life cycle
		protected void StartLevel()
		{
			Respawn();
		}

		private readonly HashSet<Collider> safeHouses = new();

		public void OnProtagonistEnterSafehouseTrigger(Collider trigger)
		{
			safeHouses.Add(trigger);
			UpdateTimerState();
			if(trigger == dormitoryRange)
				PassLevel();
		}

		public void OnProtagonistExitSafehouseTrigger(Collider trigger)
		{
			safeHouses.Remove(trigger);
			UpdateTimerState();
		}
		#endregion

		#region Functions
		public void PickUpBox()
		{
			if(boxPickedUp)
			{
				Debug.LogWarning("Already picked up a box. Cannot pick up more.");
				return;
			}
			boxPickedUp = true;
			Debug.Log("Picked up a box.");
		}

		public void DeliverBox()
		{
			if(!boxPickedUp)
			{
				Debug.LogWarning("No box picked up. Unable to deliver.");
				return;
			}
			boxPickedUp = false;
			LoadLevel(levelIndex);
			Debug.Log("Delivered a box.");
		}

		private void UnloadLevel()
		{
			if(currentLevel != null)
			{
				Destroy(currentLevel.gameObject);
				currentLevel = null;
			}
		}

		private void LoadLevel(int index, bool respawn = false)
		{
			UnloadLevel();
			// Check for invalid index.
			if(index < 0 || index >= levels.Length)
			{
				Debug.LogWarning($"Trying to use a level of an invalid index ({index}).");
				levelIndex = -1;
				return;
			}
			// Load the specified level.
			levelIndex = index;
			currentLevel = Instantiate(levels[index]);
			if(respawn)
			{
				protagonist.AlignTo(warehouseSpawn);
			}
			currentLevel.gameObject.SetActive(true);
		}

		private void PassLevel()
		{
			Debug.Log($"Passed level {levels[levelIndex].name}.");
			int nextIndex = levelIndex + 1;
			if(nextIndex >= levels.Length)
			{
				Finish();
				return;
			}
			UnloadLevel();
			levelIndex = nextIndex;
		}

		private void Respawn()
		{
			safeHouses.Clear();
			if(currentLevel != null)
			{
				LoadLevel(levelIndex, true);
				Debug.Log($"Reloaded level {levels[levelIndex].name}.", currentLevel);
			}
			else
			{
				protagonist.AlignTo(dormitorySpawn);
			}
		}
		#endregion
	}
}