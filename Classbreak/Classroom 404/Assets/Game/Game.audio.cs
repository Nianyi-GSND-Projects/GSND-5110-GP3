using UnityEngine;
using System.Collections;

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
		public void PlaySoundEffect(SoundEffect sfx, float gain = 1.0f)
		{
			StartCoroutine(PlaySoundEffectCoroutine(sfx, gain));
		}

		public IEnumerator PlaySoundEffectCoroutine(SoundEffect sfx, float gain = 1.0f)
		{
			return NaniCore.AudioUtility.PlayOneShotAtCoroutine(
				sfx.clip,
				Protagonist.transform.position,
				Protagonist.transform,
				new NaniCore.AudioUtility.AudioPlayConfig()
				{
					volume = sfx.volume * gain,
					spatialBlend = 0.0f,
					range = new(1.0f, 5.0f),
				}
			);
		}
	}
}