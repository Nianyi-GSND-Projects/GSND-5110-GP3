using UnityEngine;

namespace Game
{
	public partial class Game
	{
		#region Fields
		static private Game instance;
		static public Game Instance => instance;
		#endregion

		#region Life cycle
		protected void StartSingleton()
		{
			instance = this;
		}

		protected void EndSingleton()
		{
			instance = null;
		}

		#endregion
	}
}