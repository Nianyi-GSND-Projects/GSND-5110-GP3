using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/App")]
	public class App : ScriptableObject
	{
		public static void StartGame()
		{
			SceneManager.LoadScene("Main Game");
		}

		public static void EndGame()
		{
			SceneManager.LoadScene("End Menu");
		}

		public static void ReturnToStartMenu()
		{
			SceneManager.LoadScene("Start Menu");
		}

		public static void Quit()
		{
#if UNITY_EDITOR
			if(Application.isEditor)
			{
				UnityEditor.EditorApplication.isPlaying = false;
				return;
			}
#endif
			Application.Quit();
		}
	}
}
