using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public partial class Game : MonoBehaviour
	{
		#region Serialized fields
		[Header("General")]
		[NaughtyAttributes.Expandable]
		[SerializeField] private GameSettings settings;

		[Header("Debug")]
		[SerializeField] private bool skipStartingAnimation;
		#endregion

		#region Fields
		private Protagonist protagonist;
		private readonly HashSet<Classroom> currentlyOverlappingClassrooms = new();
		private System.Action onPlayerExitClassroom;
		#endregion

		#region Properties
		public Protagonist Protagonist => protagonist;
		public GameSettings Settings => settings;
		private bool IsInAnyClassroom => currentlyOverlappingClassrooms.Count > 0;
		#endregion

		#region Unity events
		protected void Awake()
		{
			StartSingleton();
		}

		protected void OnDestroy()
		{
			EndSingleton();
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
			currentlyOverlappingClassrooms.Add(classroom);

			if(CurrentLevel != null)
			{
				if(classroom == CurrentLevel.Destination)
					PassCurrentLevel();
			}
		}

		protected void OnPlayerExitClassroom(Classroom classroom)
		{
			currentlyOverlappingClassrooms.Remove(classroom);
			onPlayerExitClassroom?.Invoke();
		}
		#endregion

		#region Life cycle
		private IEnumerator GameStartCoroutine()
		{
			yield return new WaitForEndOfFrame();
			RevertScene();
			MovementGuidanceVisible = false;


			if(Application.isEditor)
			{
				if(!skipStartingAnimation)
				{
					startAnimation.Play();
					yield return new WaitForEndOfFrame();
					startAnimation.Pause();
				}
			}

			yield return new WaitForSeconds(1.0f);
			PlaySoundEffect(Settings.classDismissBell);

			if(Application.isEditor)
			{
				if(!skipStartingAnimation)
					yield return new AnimationUtiliity.WaitTillAnimationEnds(startAnimation);
			}

			MovementGuidanceVisible = true;
			StartCoroutine(WaitForNextLevelToStartCoroutine());
		}

		private IEnumerator FinishGameCoroutine()
		{
			Debug.Log($"All levels are passed. Game finished.");
			yield break;

			// TODO
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
		#endregion
	}
}
