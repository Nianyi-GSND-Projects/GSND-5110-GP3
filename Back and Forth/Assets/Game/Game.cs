using UnityEngine;

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

		#region Life cycle
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
			// Hide all editing levels.
			foreach(var level in FindObjectsByType<Level>(FindObjectsSortMode.None)) {
				level.gameObject.SetActive(false);
			}

			protagonist = FindObjectOfType<Protagonist>();

			StartLevel();

			Debug.Log("Game Started.");
		}
		#endregion
	}
}
