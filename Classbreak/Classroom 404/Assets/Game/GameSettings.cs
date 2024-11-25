using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game Settings")]
	public class GameSettings : ScriptableObject
	{
		[Header("Level")]
		[Tooltip("How long a level lasts.")]
		[Min(0)] public float levelTime = 60.0f;
		[Tooltip("When the timer becomes red.")]
		[Min(0)] public float warningTime = 10.0f;
		[Tooltip("How soon will the timer start after showing the phone notification.")]
		[Min(0)] public float notificationTime = 3.0f;
		[Tooltip("The waiting time between player reaching the destination and the lights in the room going off.")]
		[Min(0)] public float waitAfterPassing = 2.0f;
		[Tooltip("The time to let the player know they died before restarting the level.")]
		[Min(0)] public float timeOutDelay = 1.0f;

		[Header("Audio")]
		public SoundEffect mobileNotification;
		public SoundEffect classDismissBell;
	}
}
