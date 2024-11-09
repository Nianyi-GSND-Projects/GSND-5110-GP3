using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Timer")]
		[SerializeField][Min(0)] private float levelTime = 60.0f;
		[SerializeField] private Text timerText;
		[SerializeField] private RectTransform timerBarInnerArea;
		[SerializeField] private RectTransform timerBarFill;
		#endregion

		#region Properties
		public float TimerPercentage
		{
			set
			{
				value = Mathf.Clamp01(value);

				var size = value * timerBarInnerArea.rect.size.x;
				timerBarFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			}
		}

		public float TimeRemaining
		{
			set
			{
				int minute = Mathf.FloorToInt(value / 60.0f);
				value -= minute * 60.0f;
				int seconds = Mathf.CeilToInt(value);
				timerText.text = $"{minute}:{seconds}";
			}
		}
		#endregion

		#region Fields
		private Coroutine timerCoroutine;
		#endregion

		#region Life cycle
		private void StartTimer() {
			ResetTimer();
		}
		#endregion

		#region Functions
		private IEnumerator TimerCoroutine() {
			TimerPercentage = 1.0f;
			TimeRemaining = levelTime;
			for(float start = Time.time, elapsed; (elapsed = Time.time - start) < levelTime; ) {
				yield return new WaitForEndOfFrame();
				TimerPercentage = 1.0f - elapsed / levelTime;
				TimeRemaining = levelTime - elapsed;
			}
			TimerPercentage = 0.0f;
			TimeRemaining = 0.0f;
			OnTimeUp();
		}

		private void RunTimer() {
			if(timerCoroutine != null)
				return;
			ResetTimer();
			timerCoroutine = StartCoroutine(TimerCoroutine());
		}

		private void ResetTimer() {
			if(timerCoroutine == null)
				return;

			StopCoroutine(timerCoroutine);
			timerCoroutine = null;

			TimerPercentage = 1.0f;
			TimeRemaining = levelTime;
		}

		private void OnTimeUp() {
			Debug.Log("Time's up.");
			Respawn();
		}

		private void UpdateTimerState() {
			if(safeHouses.Count == 0)
				RunTimer();
			else
				ResetTimer();
		}
		#endregion
	}
}