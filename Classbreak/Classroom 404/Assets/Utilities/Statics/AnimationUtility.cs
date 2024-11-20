using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
	public static class AnimationUtiliity
	{
		public class WaitTillAnimationEnds : CustomYieldInstruction
		{
			private PlayableDirector playable;
			private bool finished = false;

			public override bool keepWaiting => !finished;

			private void OnStopped(PlayableDirector playable) {
				if(playable != this.playable)
					return;
				finished = true;
				playable.stopped -= OnStopped;
				playable.Stop();
			}

			public WaitTillAnimationEnds(PlayableDirector playable)
			{
				this.playable = playable;
				playable.stopped += OnStopped;
				if(playable.state != PlayState.Playing)
					playable.Play();
			}
		}
	}
}