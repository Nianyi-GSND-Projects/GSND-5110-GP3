using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class MobileUi : MonoBehaviour
	{
		[SerializeField] private NaniCore.DtCarrier carrier;

		public bool Visible
		{
			get => carrier.IsOpened;
			set => carrier.IsOpened = value;
		}
	}
}