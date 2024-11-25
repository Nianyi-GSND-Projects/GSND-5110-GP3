using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Game
{
	public partial class Game
	{
		#region Serialized fields
		[Header("UI")]
		[SerializeField] private StatusUi status;
		[SerializeField] private MobileUi mobile;
		[SerializeField] private CanvasGroup movementGuidance;
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

		private bool MovementGuidanceVisible
		{
			set
			{
				movementGuidance.alpha = value ? 1.0f : 0.0f;
				if(!value)
				{
					void HideOnExitRoom()
					{
						MovementGuidanceVisible = false;
						onPlayerExitClassroom -= HideOnExitRoom;
					}
					onPlayerExitClassroom += HideOnExitRoom;
				}
			}
		}
		#endregion
	}
}