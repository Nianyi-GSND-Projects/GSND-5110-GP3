using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class Level : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private Classroom departure, destination;
		[SerializeField] private UnityEvent onStart, onEnd;
		[SerializeField] private bool overrideReverting = false;
		[ShowIf("overrideReverting")]
		[SerializeField] private UnityEvent onReverting;
		#endregion

		#region Properties
		public Classroom Departure => departure;
		public Classroom Destination => destination;
		#endregion

		#region Interfaces
		protected void OnStart()
		{
			Destination.SetDoorPlateNumber("404");
			onStart?.Invoke();
		}

		protected void OnEnd()
		{
			Game.Instance.RevertDoorPlates();
			onEnd?.Invoke();
		}

		protected void OnRevert()
		{
			if(!overrideReverting)
				onEnd?.Invoke();
			else
				onReverting?.Invoke();
		}
		#endregion
	}
}
