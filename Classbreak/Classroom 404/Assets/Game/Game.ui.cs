using UnityEngine;
using System.Collections;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("UI")]
		[SerializeField] private StatusUi status;
		[SerializeField] private MobileUi mobile;
		#endregion

		#region Functions
		private void ShowMobile(float duration = 5.0f)
		{
			StartCoroutine(ShowMobileCoroutine(duration));
		}

		private IEnumerator ShowMobileCoroutine(float duration = 5.0f)
		{
			mobile.ClassStartTime = Settings.classStartTime;
			mobile.CurrentTime = Settings.classStartTime - Settings.levelTime;
			mobile.Visible = true;
			PlaySoundEffect(Settings.mobileNotification);
			yield return new WaitForSeconds(duration);
			mobile.Visible = false;
		}
		#endregion
	}
}