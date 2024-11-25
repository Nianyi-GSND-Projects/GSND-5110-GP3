using System.Collections;
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

		#region Fields
		private float startAppearingTime = 0, startGameTime = 0;
		private Coroutine tickCoroutine;
		#endregion

		#region Life cycle
		private IEnumerator TickCoroutine() {
			yield return new WaitForSeconds(startAppearingTime - Mathf.Floor(startAppearingTime));
			while(true)
			{
				float t = Time.time - startGameTime;
				Timer = startAppearingTime + t;
				float gain = Game.Instance.Settings.clockTickGainCurve.Evaluate(t / Game.Instance.Settings.levelTime);
				Game.Instance.PlaySoundEffect(Game.Instance.Settings.clockTick, gain);
				yield return new WaitForSeconds(1.0f);
			}
		}
		#endregion

		#region Properties
		public bool Visible
		{
			get => carrier.IsOpened;
			set
			{
				if(value)
				{
					carrier.IsOpened = true;
					if(tickCoroutine != null)
					{
						StopCoroutine(tickCoroutine);
						tickCoroutine = null;
					}
					tickCoroutine = StartCoroutine(TickCoroutine());
				}
				else
				{
					carrier.IsOpened = false;
					StopCoroutine(tickCoroutine);
					tickCoroutine = null;
				}
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
