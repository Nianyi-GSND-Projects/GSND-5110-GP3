using UnityEngine;
using System.Linq;
using System.Collections;

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
		private Coroutine passLevelCoroutine = null;
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

			// End current level.
			if(CurrentLevel != null)
				EndCurrentLevel();

			// Start new level.
			currentLevelIndex = index;
			CurrentLevel.gameObject.SetActive(true);
			CurrentLevel.SendMessage("OnStart");

			levelRunningCoroutine = StartCoroutine(LevelRunningCoroutine(CurrentLevel));

			Debug.Log($"Level \"{CurrentLevel.name}\" started.");
		}

		private void EndCurrentLevel(bool revert = false)
		{
			if(CurrentLevel == null)
				return;

			// Stop current level.
			if(levelRunningCoroutine != null)
			{
				StopCoroutine(levelRunningCoroutine);
				levelRunningCoroutine = null;
			}
			status.Visible = false;

			string levelName = CurrentLevel.name;

			CurrentLevel.SendMessage(!revert ? "OnEnd" : "OnRevert");
			CurrentLevel.gameObject.SetActive(false);
			currentLevelIndex = null;

			Debug.Log($"Level \"{levelName}\" {(!revert ? "ended" : "reverted")}.");
		}

		private void StartNextLevel()
		{
			if(passLevelCoroutine != null)
				return;

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

		private void PassCurrentLevel()
		{
			if(passLevelCoroutine != null)
				return;

			passLevelCoroutine = StartCoroutine(PassCurrentLevelCoroutine());
		}

		private IEnumerator PassCurrentLevelCoroutine()
		{
			Debug.Log($"Level \"{CurrentLevel.name}\" passed.");
			lastPassedLevelIndex = currentLevelIndex;

			if(lastPassedLevelIndex + 1 == levels.Length)
			{
				StartCoroutine(FinishGameCoroutine());
				yield break;
			}

			// Light up the previous level's destination classroom.
			SetLastPassedLevelLight(true);

			EndCurrentLevel();

			yield return new WaitForSeconds(1.0f);

			// Turn off the lights in the just-passed level's destination classroom.
			SetLastPassedLevelLight(false);

			passLevelCoroutine = null;
		}

		private IEnumerator LevelRunningCoroutine(Level level)
		{
			float levelTime = defaultLevelTime;

			// Show the mobile notification.
			ShowMobile();

			// Wait for a few seconds.
			yield return new WaitForSeconds(3.0f);

			// Show the status UI.
			status.RemainingTime = levelTime;
			status.Warning = false;
			status.Visible = true;

			// Update the status UI.
			for(float startTime = Time.time, elasped; (elasped = Time.time - startTime) < levelTime;)
			{
				float remaining = levelTime - elasped;
				status.RemainingTime = remaining;
				if(remaining <= warningTime)
				{
					status.Warning = true;
				}
				yield return new WaitForEndOfFrame();
			}

			// Time's out.
			status.RemainingTime = 0.0f;
			StartCoroutine(TimeOutCoroutine());
		}

		private IEnumerator TimeOutCoroutine()
		{
			Debug.Log("Time's up for this cycle, restarting the current level.");

			int index = currentLevelIndex.Value;
			EndCurrentLevel(true);

			yield return new WaitForSeconds(1.0f);

			ResetPlayerPosition(index);
			RevertScene();

			SetLastPassedLevelLight(false);
		}

		private void SetLastPassedLevelLight(bool on)
		{
			if(LastPassedLevel != null)
				LastPassedLevel.Destination.LightsOn = on;
		}
		#endregion
	}
}
