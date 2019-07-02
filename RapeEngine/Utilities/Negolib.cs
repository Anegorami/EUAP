using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RapeEngine {
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
			return Regex.Match(filename, Regex.Escape(@"\") + @"(.+)\.").Groups[1].Value;
		}
		
		/// <summary>
		/// String to double conversion function.
		/// Because I don't want to write "using System.Globalization;" EVERYWHERE.
		/// </summary>
		/// <param name="value">String to convert. Must use the dot as the delimeter.</param>
		/// <returns>Destringed Double.</returns>
		public static double StringToDouble(string value) {
			return double.Parse(value, CultureInfo.InvariantCulture);
		}
	}
}
