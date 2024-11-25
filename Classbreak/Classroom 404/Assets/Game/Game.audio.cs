using UnityEngine;

namespace Game
{
	public partial class Game
	{
		[Header("Audio")]
		[SerializeField] private AudioSource mobileNotificationSound;

		public void PlayMobileNotificationSound() {
			mobileNotificationSound.time = 0.0f;
			mobileNotificationSound.Play();
		}
	}
}