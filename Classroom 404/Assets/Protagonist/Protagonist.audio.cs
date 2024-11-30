using UnityEngine;

namespace Game
{
	public partial class Protagonist
	{
		#region Serialized fields
		[Header("Audio")]
		[SerializeField] private AudioSource footstepSource;
		[SerializeField] private AudioClip[] footstepSounds;
		#endregion

		#region Interfaces
		public void PlayFootStep() {
			footstepSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
		}
		#endregion
	}
}