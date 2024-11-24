using UnityEngine;
using System.Collections;
using System.Linq;

namespace Game
{
	public partial class Game : MonoBehaviour
	{
		#region Fields
		private Protagonist protagonist;
		[SerializeField] private GameSettings settings;
		#endregion

		#region Properties
		public Protagonist Protagonist => protagonist;
		public GameSettings Settings => settings;
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
			if(CurrentLevel != null)
			{
				if(classroom == CurrentLevel.Destination)
					PassCurrentLevel();
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
			yield return new AnimationUtiliity.WaitTillAnimationEnds(startAnimation);
			StartNextLevel();
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
			yield return new WaitForSeconds(duration);
			mobile.Visible = false;
		}
		#endregion
	}
}
