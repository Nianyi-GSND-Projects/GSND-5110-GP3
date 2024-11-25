using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Animation")]
		[SerializeField] private PlayableDirector startAnimation;
		#endregion
	}
}