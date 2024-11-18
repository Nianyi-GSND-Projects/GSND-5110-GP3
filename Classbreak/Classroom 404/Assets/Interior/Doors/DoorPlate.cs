using UnityEngine;

namespace Game
{
	public class DoorPlate : MonoBehaviour
	{
		[SerializeField] private TMPro.TMP_Text text;

		public string Number {
			get => text.text;
			set => text.text = value;
		}
	}
}
