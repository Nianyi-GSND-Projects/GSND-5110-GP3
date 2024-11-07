using UnityEngine;
using UnityEngine.Events;
using System;

namespace Game
{
	public class Level : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private UnityEvent onEnter, onExit;
		#endregion

		#region Properties
		public Transform SpawnPoint => spawnPoint;
		#endregion

		#region Interfaces
		public Action OnPass;
		// Unity event exposure.
		public void Pass()
		{
			OnPass.Invoke();
		}

		public Action OnReload;
		// Unity event exposure.
		public void Reload() {
			OnReload?.Invoke();
		}
		#endregion
	}
}
