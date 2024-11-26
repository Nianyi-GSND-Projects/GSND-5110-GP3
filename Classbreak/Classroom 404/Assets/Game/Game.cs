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
		[SerializeField] private UnityEngine.InputSystem.PlayerInput gameInput;

		[Header("Debug")]
		[SerializeField] private bool skipStartingAnimation;
		#endregion

		#region Fields
		private bool gameStarted = false;
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
			UiStart();

			StartCoroutine(GameStartCoroutine());
			Debug.Log("Game started.");
		}

		protected void OnStartGame() {
			gameInput.currentActionMap = null;
			gameStarted = true;
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
			gameStarted = false;
			Protagonist.ControlEnabled = false;
			Protagonist.EyelidOpenness = 0.0f;
			RevertScene();
			MovementGuidanceVisible = false;

			yield return new WaitForEndOfFrame();

			startUi.gameObject.SetActive(true);
			yield return new WaitUntil(() => gameStarted);
			startUi.gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;

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

			Protagonist.ControlEnabled = true;
			Protagonist.EyelidOpenness = 1.0f;

			MovementGuidanceVisible = true;
			StartCoroutine(WaitForNextLevelToStartCoroutine());
		}

		private IEnumerator FinishGameCoroutine()
		{
			Debug.Log($"All levels are passed. Game finished.");

			yield return new WaitForSeconds(2.0f);

			Protagonist.ControlEnabled = false;
			Cursor.lockState = CursorLockMode.None;
			endUi.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
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
