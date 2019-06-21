using System;
using System.Windows.Forms;

namespace RapeEngine {
	/// <summary>
	/// Main form.
	/// </summary>
	public sealed partial class MainForm: Form {
		Audio audio;
		
		/// <summary>
		/// Basic constructor.
		/// </summary>
		public MainForm() {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			Audio.init(Handle);
			audio = Audio.getInstance();
		}
		
		/// <summary>
		/// Update method. SHOULD be replaced with one-per-frame update event.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		public void step(object sender, EventArgs args) {
			audio.update();
		}
		
		/// <summary>
		/// Music test button.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		public void musicTest(object sender, EventArgs args) {
			audio.playBGM("test");
		}
	}
}
