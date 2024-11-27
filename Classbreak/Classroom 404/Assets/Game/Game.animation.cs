using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("Animation")]
		[SerializeField] private PlayableDirector startAnimation;
		[SerializeField] private PlayableDirector failAnimation;
		[SerializeField] private PlayableDirector endAnimation;
		#endregion
	}
}