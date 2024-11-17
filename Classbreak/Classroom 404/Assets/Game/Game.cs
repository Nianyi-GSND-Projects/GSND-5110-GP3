using UnityEngine;
using System.Collections;

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

			Debug.Log("Game started.");
		}
		#endregion

		#region Life cycle
		protected void OnPlayerEnterClassroom(Classroom classroom)
		{
			if(CurrentLevel != null)
			{
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

		private IEnumerator LevelRunningCoroutine(Level level)
		{
			float levelTime = defaultLevelTime;

			// Show the status UI.
			status.RemainingTime = levelTime;
			status.Destination = CurrentLevel.Destination.name;
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
			EndCurrentLevel();

			yield return new WaitForSeconds(1.0f);

			ResetPlayerPosition(index);
		}
		#endregion
	}
}
