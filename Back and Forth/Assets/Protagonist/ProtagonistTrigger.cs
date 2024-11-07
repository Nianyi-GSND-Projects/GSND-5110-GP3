using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ProtagonistTrigger : MonoBehaviour {
	public System.Action OnEnter, OnExit;
	[SerializeField] private UnityEvent onEnter, onExit;

	protected void Start() {
		OnEnter += () => onEnter?.Invoke();
		OnExit += () => onExit?.Invoke();
	}

	protected void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag != "Player")
			return;
		OnEnter?.Invoke();
	}

	protected void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag != "Player")
			return;
		OnExit?.Invoke();
	}
}
