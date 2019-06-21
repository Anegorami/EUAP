using System;
using System.Text.RegularExpressions;

namespace RapeEngine
{
	/// <summary>
	/// Just a small static class for some useful stuff.
	/// </summary>
	public static class Negolib {
		/// <summary>
		/// A simple function that makes keys out of filenames.
		/// It'll make "test" out of "bgm\test.ogg".
		/// </summary>
		/// <param name="filename">Filename to make a key from.</param>
		/// <returns>Key.</returns>
		public static string MakeKey(string filename) {
			// Oddly enough, () have no power here.
			string value = Regex.Match(filename, Regex.Escape("\\") + ".+" + Regex.Escape(".")).Value;
			return value.Substring(1, value.Length - 2);
		}
	}
}
