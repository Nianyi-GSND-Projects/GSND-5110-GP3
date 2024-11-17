using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class Revertable : MonoBehaviour
	{
		[SerializeField] private UnityEvent onRevert;

		protected void Revert() {
			onRevert.Invoke();
		}
	}
}
