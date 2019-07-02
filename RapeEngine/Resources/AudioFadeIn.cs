using System;
using RapeEngine.Components;
using RapeEngine.Resources;

namespace RapeEngine.Resources
{
	/// <summary>
	/// FadeIn effect for audio samples.
	/// </summary>
	public class AudioFadeIn: AudioEffect {
		/// <summary>
		/// Sample to apply the effect ot.
		/// </summary>
		readonly AudioSample sample;
		
		/// <summary>
		/// Timer to control the effect.
		/// </summary>
		int timer;
		
		/// <summary>
		/// Initial timer value.
		/// </summary>
		readonly int timer_max;
		
		/// <summary>
		/// Initial sample volume.
		/// </summary>
		readonly float volume;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_sample">Subject of the effect.</param>
		/// <param name="_timer">Effect time.</param>
		public AudioFadeIn(AudioSample _sample, int _timer) {
			sample = _sample;
			timer = _timer;
			timer_max = _timer;
			volume = sample.Volume;
			sample.Volume = 0;
		}
		
		/// <summary>
		/// Update method.
		/// </summary>
		/// <param name="dt">Time from the last call (in milliseconds).</param>
		public override void Update(int dt) {
			if (AudioManager.IsMEPlaying) {
				sample.Volume = 0;
				return;
			}
			
			timer -=dt;
			float sub_volume = Math.Max(((float) timer / (float) timer_max) * volume, 0);
			sample.Volume = volume - sub_volume;
			Remove |= Math.Abs(sub_volume) < float.Epsilon;
		}
	}
}
