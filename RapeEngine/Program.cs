using System;
using System.Windows.Forms;

namespace RapeEngine {
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	sealed class Program {
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
