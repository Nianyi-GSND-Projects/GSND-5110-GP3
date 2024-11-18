using UnityEngine;
using System.Collections;
using System.Linq;

namespace Game
{
	public partial class Game : MonoBehaviour
	{
		#region Singleton
		static private Game instance;
		static public Game Instance => instance;
		#endregion

		#region Fields
		private Protagonist protagonist;
		#endregion

		#region Properties
		public Protagonist Protagonist => protagonist;
		#endregion

		#region Unity events
		protected void Awake()
		{
			instance = this;
		}

		protected void OnDestroy()
		{
			instance = null;
		}

		protected void Start()
		{
			protagonist = FindObjectOfType<Protagonist>();
			LevelStart();

			StartCoroutine(GameStartCoroutine());
			Debug.Log("Game started.");
		}
		#endregion

		#region Message handlers
		protected void OnPlayerEnterClassroom(Classroom classroom)
		{
			if(CurrentLevel != null)
			{
				if(classroom == CurrentLevel.Destination)
					StartCoroutine(PassLevelCoroutine());
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

		#region Life cycle
		private IEnumerator GameStartCoroutine()
		{
			RevertScene();
			ResetPlayerPosition(0);
			yield return new WaitForSeconds(2.0f);
			StartNextLevel();
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

		private void StopRunningLevel()
		{
			if(levelRunningCoroutine != null)
			{
				StopCoroutine(levelRunningCoroutine);
				levelRunningCoroutine = null;
			}
			status.Visible = false;
		}

		private IEnumerator PassLevelCoroutine()
		{
			Debug.Log($"Level \"{CurrentLevel.name}\" passed.");
			lastPassedLevelIndex = currentLevelIndex;
			StopRunningLevel();

			yield return new WaitForSeconds(1.0f);

			if(lastPassedLevelIndex + 1 != levels.Length)
			{
				EndCurrentLevel();
			}
			else
			{
				Debug.Log($"All levels are passed. Game finished.");
				// TODO: Finish the game.
			}
		}

		private IEnumerator TimeOutCoroutine()
		{
			Debug.Log("Time's up for this cycle, restarting the current level.");

			int index = currentLevelIndex.Value;
			EndCurrentLevel();

			yield return new WaitForSeconds(1.0f);

			ResetPlayerPosition(index);
			RevertScene();
		}
		#endregion

		#region Functions
		private void RevertScene()
		{
			foreach(var revertable in FindObjectsOfType<MonoBehaviour>().OfType<IRevertable>())
				revertable.Revert();

			Debug.Log("Scene reverted.");
		}

		public void RevertDoorPlates()
		{
			foreach(var classroom in FindObjectsOfType<Classroom>())
				classroom.RevertDoorPlateNumber();
		}

		private void ShowMobile(float duration = 5.0f)
		{
			StartCoroutine(ShowMobileCoroutine(duration));
		}

		private IEnumerator ShowMobileCoroutine(float duration = 5.0f)
		{
			mobile.Visible = true;
			yield return new WaitForSeconds(duration);
			mobile.Visible = false;
		}
		#endregion
	}
}
