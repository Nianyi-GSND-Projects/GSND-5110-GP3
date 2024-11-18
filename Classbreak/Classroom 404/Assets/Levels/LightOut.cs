using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Game
{
	public class LightOut : MonoBehaviour, IRevertable
	{
		private struct Record
		{
			public bool active;
			public float intensity;
		}

		private Dictionary<Light, Record> records = null;
		private Coroutine coroutine = null;

		public void TurnOffAllLights(float duration)
		{
			records = new();

			foreach(var light in FindObjectsOfType<Light>())
			{
				records.Add(light, new()
				{
					active = true,
					intensity = light.intensity,
				});
			}

			StartCoroutine(TurnOffAllLightsCoroutine(duration));
		}

		private IEnumerator TurnOffAllLightsCoroutine(float duration)
		{
			for(float startTime = Time.time, elasped; (elasped = Time.time - startTime) < duration;)
			{
				float t = elasped / duration;
				foreach(var (light, record) in records)
					light.intensity = Mathf.Lerp(record.intensity, 0, t);

				yield return new WaitForEndOfFrame();
			}

			foreach(var (light, _) in records)
				light.gameObject.SetActive(false);
		}

		public void Revert()
		{
			if(records == null)
				return;

			if(coroutine != null)
			{
				StopCoroutine(coroutine);
				coroutine = null;
			}

			foreach(var (light, record) in records)
			{
				light.intensity = record.intensity;
				light.gameObject.SetActive(record.active);
			}

			records = null;
		}
	}
}
