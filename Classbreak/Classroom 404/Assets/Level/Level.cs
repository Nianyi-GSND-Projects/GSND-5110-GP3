using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class Level : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private Classroom departure, destination;
		[SerializeField] private UnityEvent onStart, onEnd;
		#endregion

		#region Properties
		public Classroom Departure => departure;
		public Classroom Destination => destination;
		#endregion

		#region Message handlers
		protected void OnStart() {
			onStart?.Invoke();
		}

		protected void OnEnd() {
			onEnd?.Invoke();
		}
		#endregion
	}
}
