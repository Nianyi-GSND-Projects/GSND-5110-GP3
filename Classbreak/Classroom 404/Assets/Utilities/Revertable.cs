using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public interface IRevertable {
		public void Revert();
	}

	public class Revertable : MonoBehaviour, IRevertable
	{
		[SerializeField] private UnityEvent onRevert;

		public void Revert() {
			onRevert.Invoke();
		}
	}
}
