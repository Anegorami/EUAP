using System;
using System.Windows.Forms;

namespace RapeEngine {
	/// <summary>
	/// Main form.
	/// </summary>
	public sealed partial class MainForm: Form {
		/// <summary>
		/// Basic constructor.
		/// </summary>
		public MainForm() {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			Audio.Init(Handle);
		}
		
		/// <summary>
		/// Update method. SHOULD be replaced with one-per-frame update event.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void Step(object sender, EventArgs args) {
			Audio.Update(0.1);
		}
		
		/// <summary>
		/// Music test method.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void MusicTest(object sender, EventArgs args) {
			if (!Audio.IsBGMPlaying()) {
				Audio.PlayBGM("test");
			} else {
				Audio.StopBGM();
			}
		}
		
		/// <summary>
		/// Sound test method.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void EffectTest(object sender, EventArgs args) {
			Audio.PlaySE("test");
		}
	}
}
