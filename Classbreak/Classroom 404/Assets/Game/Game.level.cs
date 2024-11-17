using UnityEngine;
using System.Linq;
using System;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Level")]
		[SerializeField][Min(0)] private float defaultLevelTime = 60.0f;
		[SerializeField] private Level[] levels;
		#endregion

		#region Fields
		private int? currentLevelIndex = null;
		private int? lastPassedLevelIndex = null;
		#endregion

		#region Properties
		private Level CurrentLevel
		{
			get
			{
				if(currentLevelIndex == null)
					return null;
				if(currentLevelIndex < 0 || currentLevelIndex >= levels.Length)
				{
					currentLevelIndex = null;
					return null;
				}
				return levels[currentLevelIndex.Value];
			}
		}
		private Level LastPassedLevel
		{
			get
			{
				if(lastPassedLevelIndex == null)
					return null;
				if(lastPassedLevelIndex < 0 || lastPassedLevelIndex >= levels.Length)
				{
					lastPassedLevelIndex = null;
					return null;
				}
				return levels[lastPassedLevelIndex.Value];
			}
		}
		#endregion

		#region Life cycle
		private void LevelStart()
		{
			levels = levels.Where(x => x != null).ToArray();
			foreach(var level in levels)
			{
				level.gameObject.SetActive(false);
			}
			Invoke("StartFirstLevel", 2.0f);
		}
		#endregion

		#region Functions
		protected void StartFirstLevel()
		{
			Debug.Log("Starting first level.");
			lastPassedLevelIndex = null;
			currentLevelIndex = null;
			StartLevel(0, false);
		}

		private void StartLevel(int index, bool resetPlayer)
		{
			if(index < 0 || index >= levels.Length)
			{
				Debug.LogError($"Level index {index} is out of range.");
				return;
			}

			if(CurrentLevel != null)
				EndCurrentLevel();

			currentLevelIndex = index;
			CurrentLevel.gameObject.SetActive(true);
			if(resetPlayer)
			{
				Transform spawnPoint = null;
				if(CurrentLevel.Departure != null)
					spawnPoint = CurrentLevel.Departure.SpawnPoint;
				else if(LastPassedLevel != null && LastPassedLevel.Destination != null)
					spawnPoint = LastPassedLevel.Destination.SpawnPoint;
				if(spawnPoint != null)
					Protagonist.AlignTo(spawnPoint);
			}
			CurrentLevel.SendMessage("OnStart");

			Debug.Log($"Level \"{CurrentLevel.name}\" started.");
		}

		private void EndCurrentLevel()
		{
			if(CurrentLevel == null)
				return;

			CurrentLevel.SendMessage("OnEnd");
			CurrentLevel.gameObject.SetActive(false);
			currentLevelIndex = null;
		}

		private void PassLevel()
		{
			Debug.Log($"Level \"{CurrentLevel.name}\" passed.");
			lastPassedLevelIndex = currentLevelIndex;
			EndCurrentLevel();
		}

		private void RestartCurrentLevel()
		{
			if(CurrentLevel == null)
			{
				Debug.LogError("No active level, cannot restart current level.");
				return;
			}
			Debug.Log($"Restarting level \"{CurrentLevel.name}\".");
			int index = currentLevelIndex.Value;
			EndCurrentLevel();
			StartLevel(index, true);
		}

		private void StartNextLevel() {
			int index = 0;
			if(lastPassedLevelIndex != null)
				index = lastPassedLevelIndex.Value + 1;
			if(index >= levels.Length) {
				Debug.LogError($"All levels are passed, cannot start next level.");
				return;
			}

			StartLevel(index, false);
		}
		#endregion

		#region Message handlers
		protected void OnPlayerEnterClassroom(Classroom classroom)
		{
			if(CurrentLevel != null) {
				if(classroom == CurrentLevel.Destination)
					PassLevel();
			}
		}

		protected void OnPlayerExitClassroom(Classroom classroom)
		{
			if(CurrentLevel == null)
			{
				StartNextLevel();
			}
		}
		#endregion
	}
}
