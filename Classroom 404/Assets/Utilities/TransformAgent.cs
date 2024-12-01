using UnityEngine;

namespace Game
{
	public class TransformAgent : MonoBehaviour
	{
		public Vector3 LocalEulerAngles
		{
			get => transform.localEulerAngles;
			set => transform.localEulerAngles = value;
		}

		public void SetLEAZ(float value)
		{
			var lae = LocalEulerAngles;
			lae.z = value;
			LocalEulerAngles = lae;
		}
	}
}
