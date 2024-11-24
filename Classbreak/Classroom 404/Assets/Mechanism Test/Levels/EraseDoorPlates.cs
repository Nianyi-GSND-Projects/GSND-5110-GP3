using UnityEngine;

namespace Game
{
	public class EraseDoorPlates : MonoBehaviour
	{
		public void EraseAllDoorPlates()
		{
			foreach(var doorplate in FindObjectsOfType<DoorPlate>())
				doorplate.Number = "";
		}
	}
}
