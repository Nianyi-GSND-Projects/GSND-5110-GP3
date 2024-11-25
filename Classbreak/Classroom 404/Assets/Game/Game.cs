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

#if DEBUG
		[Header("Debug")]
		[SerializeField] private bool skipStartingAnimation;
#endif
		#endregion

		#region Fields
		private Protagonist protagonist;
		private readonly HashSet<Classroom> currentlyOverlappingClassrooms = new();
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
		}
		#endregion

		#region Life cycle
		private IEnumerator GameStartCoroutine()
		{
			RevertScene();
			PlaySoundEffect(Settings.classDismissBell);
#if DEBUG
			if(!skipStartingAnimation)
#endif
				yield return new AnimationUtiliity.WaitTillAnimationEnds(startAnimation);
			StartCoroutine(WaitForNextLevelToStartCoroutine());
		}

		private IEnumerator FinishGameCoroutine()
		{
			EndCurrentLevel();

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

		private void ShowMobile(float duration = 5.0f)
		{
			StartCoroutine(ShowMobileCoroutine(duration));
		}

		private IEnumerator ShowMobileCoroutine(float duration = 5.0f)
		{
			mobile.Visible = true;
			PlaySoundEffect(Settings.mobileNotification);
			yield return new WaitForSeconds(duration);
			mobile.Visible = false;
		}
		#endregion
	}
}
