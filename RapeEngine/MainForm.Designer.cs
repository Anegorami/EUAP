using System.ComponentModel;
using System.Windows.Forms;

namespace RapeEngine
{
	sealed partial class MainForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		IContainer components;
		
		/// <summary>
		/// Button for the music test.
		/// </summary>
		Button music_test;
		
		/// <summary>
		/// Button for the sound test.
		/// </summary>
		Button test_se;
		
		/// <summary>
		/// Update timer.
		/// </summary>
		Timer update_timer;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed, false otherwise.</param>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// 
		/// Bullshit. Just keep your hands clean and everything will be OK.
		/// </summary>
		void InitializeComponent() {
			// Init.
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
			
			music_test = new Button();
			test_se = new Button();
			update_timer = new Timer(components);
			
			SuspendLayout();
			
			// Music test.
			music_test.Location = new System.Drawing.Point(88, 42);
			music_test.Name = "music_test";
			music_test.Size = new System.Drawing.Size(103, 23);
			music_test.TabIndex = 0;
			music_test.Text = "Play BGM test.ogg";
			music_test.UseVisualStyleBackColor = true;
			music_test.Click += MusicTest;
			
			// Effect test.
			test_se.Location = new System.Drawing.Point(88, 102);
			test_se.Name = "test_se";
			test_se.Size = new System.Drawing.Size(103, 28);
			test_se.TabIndex = 1;
			test_se.Text = "Play SE test.ogg";
			test_se.UseVisualStyleBackColor = true;
			test_se.Click += EffectTest;
			
			// Update timer.
			update_timer.Enabled = true;
			update_timer.Tick += Step;
			
			// Main form.
			ClientSize = new System.Drawing.Size(1280, 720);
			Controls.Add(music_test);
			Controls.Add(test_se);
			Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			MaximizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Rape Engine";
			
			ResumeLayout(false);
		}
	}
}
