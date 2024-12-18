using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game Settings")]
	public class GameSettings : ScriptableObject
	{
		[Header("Level")]
		[Tooltip("How long a level lasts.")]
		[Min(0)] public float levelTime = 60.0f;
		[Tooltip("The appearing starting time of the course, in seconds.")]
		[Min(0)] public float classStartTime = 10 * 60 * 60;
		[Tooltip("When the timer becomes red.")]
		[Min(0)] public float warningTime = 10.0f;
		[Tooltip("How soon will the timer start after showing the phone notification.")]
		[Min(0)] public float notificationTime = 3.0f;
		[Tooltip("The waiting time between player reaching the destination and the class-dismissing bell ringing.")]
		[Min(0)] public float waitBeforePassing = 2.0f;
		[Tooltip("The waiting time between the class-dismissing bell ringing and the lights in the room going off.")]
		[Min(0)] public float waitAfterPassing = 2.0f;
		[Tooltip("The maximum time the player could wait in the classroom before the next level starts.")]
		[Min(0)] public float maxWaitTime = 10.0f;

		[Header("Audio")]
		public SoundEffect mobileNotification;
		public SoundEffect classDismissBell;
		public SoundEffect clockTick;
		public AnimationCurve clockTickGainCurve;
	}
}
