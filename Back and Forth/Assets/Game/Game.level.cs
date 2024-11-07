using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[SerializeField] private Level[] levels;
		[SerializeField] private int levelIndex = 0;
		#endregion

		#region Fields
		private Level currentLevel = null;
		#endregion

		#region Life cycle
		protected void StartLevel() {
			UseLevel(levelIndex, true);
		}

		public void OnProtagonistEnterPassingTrigger(Collider trigger) {
			if(trigger != currentLevel.PassingTrigger)
				return;
			PassLevel();
		}

		private HashSet<Collider> safeHouses = new();

		public void OnProtagonistEnterSafehouseTrigger(Collider trigger)
		{
			safeHouses.Add(trigger);
			UpdateTimerState();
		}

		public void OnProtagonistExitSafehouseTrigger(Collider trigger)
		{
			safeHouses.Remove(trigger);
			UpdateTimerState();
		}
		#endregion

		#region Functions
		private void UseLevel(int index, bool respawn = false)
		{
			// Unload any loaded level.
			if(currentLevel != null)
			{
				Destroy(currentLevel.gameObject);
				currentLevel = null;
			}
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
			currentLevel.OnPass += PassLevel;
			currentLevel.OnReload += ReloadLevel;
			if(respawn)
			{
				protagonist.AlignTo(currentLevel.SpawnPoint);
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
			UseLevel(nextIndex);
		}

		private void ReloadLevel()
		{
			safeHouses.Clear();
			UseLevel(levelIndex, true);
			Debug.Log($"Reloaded level {levels[levelIndex].name}.", currentLevel);
		}
		#endregion
	}
}