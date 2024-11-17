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

		#region Properties
		public Protagonist Protagonist => protagonist;
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
			protagonist = FindObjectOfType<Protagonist>();
			LevelStart();

			Debug.Log("Game started.");
		}
		#endregion
	}
}
