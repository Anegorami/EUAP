using System;
using System.Windows.Forms;
using Un4seen.Bass;

namespace RapeEngine {
	/// <summary>
	/// Main form.
	/// </summary>
	public sealed partial class MainForm: Form {
		/// <summary>
		/// Audio handler for playback stopping.
		/// </summary>
		int stream;
		
		/// <summary>
		/// Basic constructor.
		/// </summary>
		public MainForm() {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Handle);
		}
		
		/// <summary>
		/// Music test.
		/// </summary>
		/// <param name="sender">EventHandler parameter. Required, but not used.</param>
		/// <param name="args">EventHandler parameter. Required, but not used.</param>
		public void musicTest(object sender, EventArgs args) {
			Bass.BASS_ChannelStop(stream);
			
			stream = Bass.BASS_StreamCreateFile("test.mp3", 0, 0, BASSFlag.BASS_DEFAULT);
			Bass.BASS_ChannelPlay(stream, false);
		}
	}
}
