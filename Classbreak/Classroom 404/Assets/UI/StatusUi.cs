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
		[Header("Text")]
		[SerializeField] private Text destination;
		[SerializeField] private Text remainingTime;
		#endregion
	}
}
