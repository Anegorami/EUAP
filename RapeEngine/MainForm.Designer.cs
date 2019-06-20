namespace RapeEngine
{
	sealed partial class MainForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// Not required for now.
		/// </summary>
		//System.ComponentModel.IContainer components;
		
		/// <summary>
		/// Button for the music test.
		/// </summary>
		System.Windows.Forms.Button music_test;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed, false otherwise.</param>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				/*if (components != null) {
					components.Dispose();
				}*/
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
			music_test = new System.Windows.Forms.Button();
			SuspendLayout();
			
			// Button.
			music_test.Location = new System.Drawing.Point(88, 102);
			music_test.Name = "music_test";
			music_test.Size = new System.Drawing.Size(103, 23);
			music_test.TabIndex = 0;
			music_test.Text = "Play test.mp3";
			music_test.UseVisualStyleBackColor = true;
			music_test.Click += musicTest;
			
			// Form.
			ClientSize = new System.Drawing.Size(284, 262);
			Controls.Add(this.music_test);
			Name = "MainForm";
			Text = "Rape Engine";
			
			ResumeLayout(false);
		}
	}
}
