using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public partial class Game
	{
		#region Serialized Fields
		[Header("UI")]
		[SerializeField] private Text timerText;
		[SerializeField] private RectTransform timerBarInnerArea;
		[SerializeField] private RectTransform timerBarFill;
		#endregion

		#region Interfaces
		public float TimerPercentage {
			set {
				value = Mathf.Clamp01(value);

				var sizeDelta = timerBarFill.sizeDelta;
				sizeDelta.x = value * timerBarInnerArea.sizeDelta.x;
				timerBarFill.sizeDelta = sizeDelta;
			}
			get => timerBarFill.sizeDelta.x / timerBarInnerArea.sizeDelta.x;
		}

		public float TimeRemaining {
			set {
				int minute = Mathf.FloorToInt(value / 60.0f);
				value -= minute * 60.0f;
				int seconds = Mathf.CeilToInt(value);
				timerText.text = $"{minute}:{seconds}";
			}
		}
		#endregion
	}
}