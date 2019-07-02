using System;

namespace RapeEngine.Resources
{
	/// <summary>
	/// A base class for the audio effects.
	/// </summary>
	public abstract class AudioEffect {
		/// <summary>
		/// Removal flag.
		/// Set it to "true" to have this effect removed.
		/// </summary>
		protected bool Remove {get; set;}
		
		/// <summary>
		/// Update method.
		/// </summary>
		/// <param name="dt">Time from the last call (in milliseconds).</param>
		public abstract void Update(int dt);
		
		/// <summary>
		/// Predicate for the removal.
		/// </summary>
		/// <param name="effect">Effect to test.</param>
		/// <returns>True is the removal flag is true, false otherwise.</returns>
		public static bool RemovalCondition(AudioEffect effect) {
			return effect.Remove;
		}
	}
}
