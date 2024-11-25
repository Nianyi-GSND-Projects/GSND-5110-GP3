using UnityEngine;
using NaniCore;

namespace Game
{
	[System.Serializable]
	public struct SoundEffect
	{
		public AudioClip clip;
		public float volume;
	}

	public partial class Game
	{
		public void PlaySoundEffect(SoundEffect sfx)
		{
			Protagonist.PlaySfxOneShot(sfx.clip, new AudioUtility.AudioPlayConfig()
			{
				volume = sfx.volume,
				spatialBlend = 0.0f,
				range = new(1.0f, 5.0f),
			});
		}
	}
}