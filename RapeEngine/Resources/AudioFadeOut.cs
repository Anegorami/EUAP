using System;

namespace RapeEngine.Resources {
	/// <summary>
	/// FadeOut effect for audio samples.
	/// </summary>
	public class AudioFadeOut: AudioEffect {
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
		public AudioFadeOut(AudioSample _sample, int _timer) {
			sample = _sample;
			timer = _timer;
			timer_max = _timer;
			volume = sample.Volume;
		}
		
		/// <summary>
		/// Update method.
		/// </summary>
		/// <param name="dt">Time from the last call (in milliseconds).</param>
		public override void Update(int dt) {
			timer -=dt;
			float new_volume = Math.Max(((float) timer / (float) timer_max) * volume, 0);
			sample.Volume = new_volume;
			if (Math.Abs(new_volume) < float.Epsilon) {
				sample.Stop();
			}
			
			Remove |= !sample.IsPlaying;
		}
	}
}
