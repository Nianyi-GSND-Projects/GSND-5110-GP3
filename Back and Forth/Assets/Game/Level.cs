using UnityEngine;
using UnityEngine.Events;
using System;

namespace Game
{
	public class Level : MonoBehaviour
	{
		#region Serialized fields
		[SerializeField] private UnityEvent onEnter, onExit;
		#endregion
	}
}
