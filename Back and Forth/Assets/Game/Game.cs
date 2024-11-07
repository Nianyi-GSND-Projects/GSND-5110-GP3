using UnityEngine;

namespace Game
{
	public partial class Game : MonoBehaviour
	{
		#region Life cycle
		protected void Start()
		{
			// Hide all editing levels.
			foreach(var level in FindObjectsByType<Level>(FindObjectsSortMode.None)) {
				level.gameObject.SetActive(false);
			}

			protagonist = FindObjectOfType<Protagonist>();

			UseLevel(levelIndex, true);

			Debug.Log("Game Started.");
		}
		#endregion

		#region Protagonist
		private Protagonist protagonist;
		#endregion

		#region Level
		[SerializeField] private Level[] levels;
		[SerializeField] private int levelIndex = 0;

		private Level currentLevel = null;

		private void UseLevel(int index, bool respawn = false)
		{
			// Unload any loaded level.
			if(currentLevel != null)
			{
				Destroy(currentLevel.gameObject);
				currentLevel = null;
			}
			// Check for invalid index.
			if(index < 0 || index >= levels.Length)
			{
				Debug.LogWarning($"Trying to use a level of an invalid index ({index}).");
				levelIndex = -1;
				return;
			}
			// Load the specified level.
			levelIndex = index;
			currentLevel = Instantiate(levels[index]);
			currentLevel.OnPass += PassLevel;
			currentLevel.OnReload += ReloadLevel;
			if(respawn) {
				protagonist.AlignTo(currentLevel.SpawnPoint);
			}
			currentLevel.gameObject.SetActive(true);
		}

		private void PassLevel()
		{
			int nextIndex = levelIndex + 1;
			if(nextIndex >= levels.Length) {
				Finish();
				return;
			}
			UseLevel(nextIndex);
		}

		private void ReloadLevel()
		{
			UseLevel(levelIndex, true);
		}

		private void Finish() {
			Debug.Log("Game finished.");
		}
		#endregion
	}
}
