using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Game
{
	public class IndividualLight : MonoBehaviour
	{
		private struct LightConfig
		{
			public float intensity;
		}

		private struct RendererConfig
		{
			public Color emissiveColor;
			public float emissionIntensity;
		}

		public struct PropertyNames
		{
			public const string useEmission = "_UseEmissiveIntensity";
			public const string emissionColor = "_EmissiveColor";
			public const string emissionIntensity = "_EmissiveIntensity";
		}

		#region Fields
		private Dictionary<HDAdditionalLightData, LightConfig> lights;
		private Dictionary<Renderer, RendererConfig> renderers;
		private float intensity = 1.0f;
		[SerializeField] private float speed = 2.0f;

		private Coroutine adjustIntensitySmoothlyCoroutine;
		#endregion

		#region Life cycle
		protected void Start()
		{
			lights = new(GetComponentsInChildren<HDAdditionalLightData>(true)
				.Select(light => new KeyValuePair<HDAdditionalLightData, LightConfig>(
					light,
					new LightConfig
					{
						intensity = light.intensity,
					}
				))
			);
			renderers = new(GetComponentsInChildren<Renderer>(true)
				.Where(renderer => renderer.sharedMaterial.GetInt(PropertyNames.useEmission) != 0)
				.Select(renderer => new KeyValuePair<Renderer, RendererConfig>(
					renderer,
					new RendererConfig
					{
						emissiveColor = renderer.material.GetColor(PropertyNames.emissionColor),
						emissionIntensity = renderer.material.GetFloat(PropertyNames.emissionIntensity),
					}
				))
			);
		}
		#endregion

		#region Properties
		private float Intensity
		{
			get => intensity;
			set
			{
				value = Mathf.Max(0, value);

				foreach(var (light, config) in lights)
				{
					light.intensity = config.intensity * value;
				}

				foreach(var (renderer, config) in renderers)
				{
					var mat = renderer.material;
					mat.SetFloat(PropertyNames.emissionIntensity, config.emissionIntensity * value);
					mat.SetColor(PropertyNames.emissionColor, config.emissiveColor * value);
				}

				intensity = value;
			}
		}
		#endregion

		#region Interfaces
		[ContextMenu("Turn On")] public void TurnOn() => AdjustIntensitySmoothly(1.0f);
		[ContextMenu("Turn Off")] public void TurnOff() => AdjustIntensitySmoothly(0.0f);
		#endregion

		#region Functions
		private void AdjustIntensitySmoothly(float targetIntensity)
		{
			if(adjustIntensitySmoothlyCoroutine != null) {
				StopCoroutine(adjustIntensitySmoothlyCoroutine);
				adjustIntensitySmoothlyCoroutine = null;
			}
			adjustIntensitySmoothlyCoroutine = StartCoroutine(AdjustIntensitySmoothlyCoroutine(targetIntensity));
		}

		private IEnumerator AdjustIntensitySmoothlyCoroutine(float targetIntensity)
		{
			float delta = targetIntensity - Intensity;
			float totalTime = Mathf.Abs(delta / speed);

			float startIntensity = Intensity;
			for(float startTime = Time.time, elapsed; (elapsed = Time.time - startTime) < totalTime; ) {
				Intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsed / totalTime);
				yield return new WaitForEndOfFrame();
			}
			Intensity = targetIntensity;

			adjustIntensitySmoothlyCoroutine = null;
		}
		#endregion
	}
}
