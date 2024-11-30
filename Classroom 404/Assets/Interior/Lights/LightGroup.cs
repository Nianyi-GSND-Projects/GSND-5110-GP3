using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

namespace Game
{
	public class LightGroup : MonoBehaviour
	{
		[SerializeField] private bool useChildGroups = true;
		[HideIf("useChildGroups")]
		[SerializeField] private List<LightGroup> subgroups = new();
		[SerializeField] private bool useChildLights = true;
		[HideIf("useChildLights")]
		[SerializeField] private List<IndividualLight> lights = new();

		private IEnumerable<LightGroup> SubGroups => !useChildGroups
			? subgroups
			: GetComponentsInChildren<LightGroup>().Where(g => g != this);
		private IEnumerable<IndividualLight> Lights => !useChildLights
			? lights
			: GetComponentsInChildren<IndividualLight>();

		[ContextMenu("Turn On")]
		public void TurnOn()
		{
			foreach(var g in SubGroups)
				g.TurnOn();
			foreach(var l in Lights)
				l.TurnOn();
		}

		[ContextMenu("Turn Off")]
		public void TurnOff()
		{
			foreach(var g in SubGroups)
				g.TurnOff();
			foreach(var l in Lights)
				l.TurnOff();
		}
	}
}
