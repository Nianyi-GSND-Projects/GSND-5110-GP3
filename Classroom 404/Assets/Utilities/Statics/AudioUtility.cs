using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NaniCore {
	public static class AudioUtility {
		public struct AudioPlayConfig {
			public Vector2 range;
			public float volume;
			public float spatialBlend;
			public AudioRolloffMode rolloffMode;

			public readonly void ApplyOn(AudioSource source) {
				if(source == null)
					return;

				source.volume = volume;

				source.minDistance = range.x;
				source.maxDistance = range.y;
				source.spatialBlend = spatialBlend;
				source.rolloffMode = rolloffMode;
			}
		}

		public static IEnumerator PlayOneShotAtCoroutine(AudioClip clip, Vector3 worldPosition, Transform under, AudioPlayConfig config) {
			if(clip == null)
				yield break;

			config.volume = Mathf.Abs(config.volume);

			GameObject player = new($"Audio ({clip.name})");
			player.transform.position = worldPosition;
			player.transform.SetParent(under, true);

			AudioSource source = player.AddComponent<AudioSource>();
			source.playOnAwake = false;
			config.ApplyOn(source);

			source.PlayOneShot(clip);

			// The AudioSource instance might be destroyed during playing.
			yield return new WaitUntil(() => {
				if(source == null)
					return true;
				return !source.isPlaying;
			});

			if(player != null)
				Object.Destroy(player);
		}
		public static IEnumerator PlayOneShotAtCoroutine(AudioClip clip, Vector3 worldPosition, Transform under) {
			AudioPlayConfig config = new() {
				range = new Vector2(1.0f, 500.0f),
				volume = 1.0f,
				spatialBlend = 1.0f,
				rolloffMode = AudioRolloffMode.Logarithmic,
			};
			return PlayOneShotAtCoroutine(clip, worldPosition, under, config);
		}

		public static void PlaySfxOneShot(this MonoBehaviour mb, AudioClip clip, AudioPlayConfig config) {
			mb.StartCoroutine(PlayOneShotAtCoroutine(clip, mb.transform.position, mb.transform, config));
		}
	}
}