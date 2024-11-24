using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game Settings")]
	public class GameSettings : ScriptableObject
	{
		[Header("Level")]
		[Tooltip("How soon will the timer start after showing the phone notification.")]
		[Min(0)] public float notificationTime = 3.0f;
		[Tooltip("The waiting time between player reaching the destination and the lights in the room going off.")]
		[Min(0)] public float waitAfterPassing = 2.0f;
		[Tooltip("The time to let the player know they died before restarting the level.")]
		[Min(0)] public float timeOutDelay = 1.0f;
	}
}
