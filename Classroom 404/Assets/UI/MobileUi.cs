using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class MobileUi : MonoBehaviour
	{
		[SerializeField] private NaniCore.DtCarrier carrier;
		[SerializeField] private Text topBarTime;
		[SerializeField] private Text classStartTime;

		public bool Visible
		{
			get => carrier.IsOpened;
			set => carrier.IsOpened = value;
		}

		public float CurrentTime
		{
			set => topBarTime.text = TimeUtility.SecondsToString(value, "hh:mm");
		}

		public float ClassStartTime {
			set => classStartTime.text = TimeUtility.SecondsToString(value, "hh:mm");
		}
	}
}