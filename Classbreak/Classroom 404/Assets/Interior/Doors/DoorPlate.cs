using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class DoorPlate : MonoBehaviour
	{
		[SerializeField] private Text text;

		public string Number {
			get => text.text;
			set => text.text = value;
		}
	}
}
