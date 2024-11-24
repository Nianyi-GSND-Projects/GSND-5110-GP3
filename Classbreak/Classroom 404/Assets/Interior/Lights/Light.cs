using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class Light : MonoBehaviour
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

			Intensity = 0.0f;
		}
		#endregion

		#region Properties
		public float Intensity
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

					var color = config.emissiveColor;
					color *= value;
					color.a = 1.0f;
					mat.SetColor(PropertyNames.emissionColor, color);
				}

				intensity = value;
			}
		}
		#endregion
	}
}
