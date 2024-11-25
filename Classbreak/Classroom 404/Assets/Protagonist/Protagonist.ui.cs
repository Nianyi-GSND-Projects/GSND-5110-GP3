using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
	public partial class Protagonist
	{
		[Header("UI")]
		[SerializeField] private Graphic focus;
		[SerializeField] private Graphic eyelids;
		[SerializeField][Min(0)] private float eyelidSpeed = 1.0f;

		public float EyelidOpenness {
			get {
				return eyelids.material.GetFloat("_Openness");
			}
			set
			{
				value = Mathf.Clamp01(value);
				eyelids.material.SetFloat("_Openness", value);
				eyelids.enabled = value != 1.0f;
			}
		}

		public void SetEyelidOpenness(float targetOpenness) {
			if(setEyelidOpennessCoroutine != null)
			{
				StopCoroutine(setEyelidOpennessCoroutine);
				setEyelidOpennessCoroutine = null;
			}
			StartCoroutine(SetEyelidOpennessCoroutine(targetOpenness));
		}

		private Coroutine setEyelidOpennessCoroutine;
		private IEnumerator SetEyelidOpennessCoroutine(float targetOpenness)
		{
			float startOpenness = EyelidOpenness;
			float totalTime = Mathf.Abs((startOpenness - targetOpenness) / eyelidSpeed);
			for(float startTime = Time.time, elapsed; (elapsed = Time.time - startTime) < totalTime;)
			{
				EyelidOpenness = Mathf.Lerp(startOpenness, targetOpenness, elapsed / totalTime);
				yield return new WaitForEndOfFrame();
			}
			EyelidOpenness = targetOpenness;
			setEyelidOpennessCoroutine = null;
		}
	}
}