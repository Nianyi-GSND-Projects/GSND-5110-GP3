using UnityEngine;
using System.Linq;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Level")]
		[SerializeField][Min(0)] private float defaultLevelTime = 60.0f;
		[SerializeField][Min(0)] private float warningTime = 10.0f;
		[SerializeField] private Level[] levels;
		#endregion

		#region Fields
		private int? currentLevelIndex = null;
		private int? lastPassedLevelIndex = null;
		private Coroutine levelRunningCoroutine = null;
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
		}
		#endregion

		#region Functions
		private void ResetPlayerPosition(int index)
		{
			Level level = levels[index];
			Debug.Log($"{level.Departure}, {level.Departure.SpawnPoint}");
			Transform spawnPoint = null;
			if(level != null && level.Departure != null)
				spawnPoint = level.Departure.SpawnPoint;
			else if(LastPassedLevel != null && LastPassedLevel.Destination != null)
				spawnPoint = LastPassedLevel.Destination.SpawnPoint;
			if(spawnPoint != null)
			{
				Debug.Log($"Resetting player's position to {spawnPoint}.", spawnPoint);
				Protagonist.AlignTo(spawnPoint);
			}
		}

		private void StartLevel(int index)
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
			CurrentLevel.SendMessage("OnStart");

			levelRunningCoroutine = StartCoroutine(LevelRunningCoroutine(CurrentLevel));

			Debug.Log($"Level \"{CurrentLevel.name}\" started.");
		}

		private void EndCurrentLevel()
		{
			if(CurrentLevel == null)
				return;

			if(levelRunningCoroutine != null)
			{
				StopCoroutine(levelRunningCoroutine);
				levelRunningCoroutine = null;
			}
			status.Visible = false;

			string levelName = CurrentLevel.name;

			CurrentLevel.SendMessage("OnEnd");
			CurrentLevel.gameObject.SetActive(false);
			currentLevelIndex = null;

			Debug.Log($"Level \"{levelName}\" ended.");
		}

		private void PassLevel()
		{
			Debug.Log($"Level \"{CurrentLevel.name}\" passed.");
			lastPassedLevelIndex = currentLevelIndex;
			EndCurrentLevel();
			if(lastPassedLevelIndex + 1 == levels.Length)
			{
				Debug.Log($"All levels are passed. Game finished.");
			}
		}

		private void StartNextLevel()
		{
			int index = 0;
			if(lastPassedLevelIndex != null)
				index = lastPassedLevelIndex.Value + 1;
			if(index >= levels.Length)
			{
				Debug.LogWarning($"All levels are passed, cannot start next level.");
				return;
			}

			StartLevel(index);
		}
		#endregion
	}
}
