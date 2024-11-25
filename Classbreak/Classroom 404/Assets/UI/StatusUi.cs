using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class StatusUi : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private NaniCore.DtCarrier carrier;
		[Header("Clock")]
		[SerializeField] private MaskableGraphic clock;
		[SerializeField] private RectTransform hourHand, minuteHand, secondHand;
		[SerializeField] private Color warningColor = (Color.red + Color.white) * 0.5f;
		[SerializeField] private Text timer;
		#endregion

		#region Fiedlds
		private float startAppearingTime = 0, startGameTime = 0;
		#endregion

		#region Life cycle
		protected void Update()
		{
			Timer = startAppearingTime + Time.time - startGameTime;
		}
		#endregion

		#region Properties
		public bool Visible
		{
			get => carrier.IsOpened;
			set
			{
				carrier.IsOpened = value;
			}
		}

		public float CurrentTime
		{
			set
			{
				startAppearingTime = value;
				startGameTime = Time.time;
			}
		}

		private float Timer
		{
			set
			{
				var t = TimeUtility.TimeRecord.FromSeconds(value);
				timer.text = t.ToString();

				var bv = Vector3.back * 6;
				hourHand.localEulerAngles = bv * 5 * t.hours;
				minuteHand.localEulerAngles = bv * t.minutes;
				secondHand.localEulerAngles = bv * t.seconds;

				bool warning = Game.Instance.Settings.classStartTime - value < Game.Instance.Settings.warningTime;
				clock.color = warning ? warningColor : Color.white;
			}
		}
		#endregion
	}
}
