using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game Agent")]
	public class GameAgent : ScriptableObject
	{
		private Game Game => Game.Instance;

		public void PickUpBox() {
			Game.PickUpBox();
		}

		public void DeliverBox()
		{
			Game.DeliverBox();
		}
	}
}
