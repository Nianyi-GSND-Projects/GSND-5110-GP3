using UnityEngine;

namespace Game
{
	public class EraseDoorPlates : MonoBehaviour
	{
		public void EraseAllDoorPlates()
		{
			foreach(var room in FindObjectsOfType<Classroom>())
			{
				foreach(var doorplate in room.DoorPlates)
					doorplate.text = "";
			}
		}
	}
}
