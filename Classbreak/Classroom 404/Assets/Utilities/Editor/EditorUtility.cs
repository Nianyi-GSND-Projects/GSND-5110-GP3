using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using System.IO;

namespace NaniCore.Bordure {
	public static class EditorUtility {
		[MenuItem("Tools/Take Screenshot")]
		public static void TakeScreenshot() {
			string directory = $"{Directory.GetParent(Application.dataPath)}/Screenshots";
			if(!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			int count = Directory.GetFiles(directory).Length;
			string path = $"{directory}/screenshot{count}.png";
			ScreenCapture.CaptureScreenshot(path);
			Debug.Log($"Screenshot saved to {path}.");
		}
	}
}