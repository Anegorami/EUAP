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
			Audio.Init(Handle);
			audio = Audio.GetInstance();
		}
		
		/// <summary>
		/// Update method. SHOULD be replaced with one-per-frame update event.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void Step(object sender, EventArgs args) {
			audio.Update();
		}
		
		/// <summary>
		/// Music test method.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void MusicTest(object sender, EventArgs args) {
			audio.PlayBGM("test");
		}
		
		/// <summary>
		/// Sound test method.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		void EffectTest(object sender, EventArgs args) {
			audio.PlaySE("test");
		}
	}
}
