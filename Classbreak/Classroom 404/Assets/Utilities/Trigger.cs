using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
	public System.Action OnEnter, OnExit;
	[SerializeField] private UnityEvent onEnter, onExit;

	protected void Start()
	{
		OnEnter += () => onEnter?.Invoke();
		OnExit += () => onExit?.Invoke();
	}

	protected void OnTriggerEnter(Collider other)
	{
		OnEnter?.Invoke();
	}

	protected void OnTriggerExit(Collider other)
	{
		OnExit?.Invoke();
	}
}
