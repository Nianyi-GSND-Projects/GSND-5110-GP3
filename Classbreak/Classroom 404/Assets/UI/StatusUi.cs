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
		[Header("Text")]
		[SerializeField] private Text destination;
		[SerializeField] private Text remainingTime;
		#endregion

		#region Properties
		public bool Visible
		{
			get => carrier.IsOpened;
			set => carrier.IsOpened = value;
		}

		public float RemainingTime
		{
			set
			{
				remainingTime.text = value.ToString();
			}
		}

		public string Destination
		{
			set => destination.text = value;
		}

		public bool Warning
		{
			set
			{
				clock.color = value ? warningColor : Color.white;
			}
		}
		#endregion
	}
}
