using System;
using System.IO;

namespace RapeEngine.Components {
	/// <summary>
	/// Static class to store the user variables.
	/// </summary>
	public static class UserVariables {
		/// <summary>
		/// Script file for variables' names storage.
		/// </summary>
		const string NAMES_FILE = "scripts\\vars.txt";
		
		/// <summary>
		/// Hardcoded flags limit.
		/// </summary>
		public const int FLAGS_MAX = 1000;
		
		/// <summary>
		/// Hardcoded variables limit.
		/// </summary>
		public const int VARIABLES_MAX = 1000;
		
		/// <summary>
		/// User flags.
		/// </summary>
		public static readonly bool[] Flag = new bool[FLAGS_MAX];
		
		/// <summary>
		/// User variables.
		/// </summary>
		public static readonly int[] Variable = new int[VARIABLES_MAX];
		
		/// <summary>
		/// Flags' names.
		/// </summary>
		public static readonly string[] FlagNames = new string[FLAGS_MAX];
		
		/// <summary>
		/// Variables' names.
		/// </summary>
		public static readonly string[] VariableNames = new string[VARIABLES_MAX];
		
		/// <summary>
		/// Method to get a flag name.
		/// </summary>
		/// <param name="index">Flag index.</param>
		/// <returns>Flag name or default.</returns>
		public static string GetFlagName(int index) {
			string val = FlagNames[index];
			return (String.IsNullOrEmpty(val))? "Flag " + (index + 1): val;
		}
		
		/// <summary>
		/// Method to get a variable name.
		/// </summary>
		/// <param name="index">Variable index.</param>
		/// <returns>Variable name or default.</returns>
		public static string GetVariableName(int index) {
			string val = VariableNames[index];
			return (String.IsNullOrEmpty(val))? "Variable " + (index + 1): val;
		}
		
		/// <summary>
		/// Method to read the names from the file.
		/// </summary>
		public static void ReadNames() {
			try {
				StreamReader input = File.OpenText(NAMES_FILE);
				for (int index = 0; index < FLAGS_MAX + VARIABLES_MAX; index++) {
					if (index < FLAGS_MAX) {
						FlagNames[index] = input.ReadLine();
					} else {
						VariableNames[index - FLAGS_MAX] = input.ReadLine();
					}
				}
				input.Close();
			} catch (FileNotFoundException) {
				return;
			}
		}
		
		/// <summary>
		/// Method to write the names to the file.
		/// </summary>
		public static void WriteNames() {
			var file = File.OpenWrite(NAMES_FILE);
			var output = new StreamWriter(file);
			foreach (string name in FlagNames) {
				output.WriteLine(name);
			}
			foreach (string name in VariableNames) {
				output.WriteLine(name);
			}
			output.Close();
			file.Close();
		}
	}
}
