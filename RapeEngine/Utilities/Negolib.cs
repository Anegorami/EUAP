using System;
using System.IO;
using System.Globalization;

namespace RapeEngine {
	/// <summary>
	/// Just a small static class for some useful stuff.
	/// </summary>
	public static class Negolib {
		/// <summary>
		/// A simple method that makes keys out of filenames.
		/// It'll make "test" out of "bgm\test.ogg".
		/// </summary>
		/// <param name="filename">Filename to make a key from.</param>
		/// <returns>Key.</returns>
		public static string MakeKey(string filename) {
			return Path.GetFileNameWithoutExtension(filename);
		}
		
		/// <summary>
		/// String to double conversion method.
		/// Because I don't want to write "using System.Globalization;" EVERYWHERE.
		/// </summary>
		/// <param name="value">String to convert. Must use the dot as the delimeter.</param>
		/// <returns>Destringed Double.</returns>
		public static double StringToDouble(string value) {
			return double.Parse(value, CultureInfo.InvariantCulture);
		}
		
		/// <summary>
		/// Method to get the extension WITHOUT the dot.
		/// </summary>
		/// <param name="filename">Path.</param>
		/// <returns>Extension.</returns>
		public static string GetExtension(string filename) {
			return Path.GetExtension(filename).Substring(1);
		}
	}
}
