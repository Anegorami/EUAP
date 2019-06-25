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
		/// Button for the BGS test.
		/// </summary>
		Button bgs_test;
		
		/// <summary>
		/// Button for the music effect test.
		/// </summary>
		Button test_me;
		
		/// <summary>
		/// Button for the sound test.
		/// </summary>
		Button test_se;
		
		/// <summary>
		/// Master volume trackbar.
		/// </summary>
		TrackBar master;
		
		/// <summary>
		/// BGM volume trackbar.
		/// </summary>
		TrackBar bgm;
		
		/// <summary>
		/// BGS volume trackbar.
		/// </summary>
		TrackBar bgs;
		
		/// <summary>
		/// ME volume trackbar.
		/// </summary>
		TrackBar me;
		
		/// <summary>
		/// SE volume trackbar.
		/// </summary>
		TrackBar se;
		
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
			var resources = new ComponentResourceManager(typeof(MainForm));
			
			music_test = new Button();
			bgs_test = new Button();
			test_me = new Button();
			test_se = new Button();
			
			master = new TrackBar();
			bgm = new TrackBar();
			bgs = new TrackBar();
			me = new TrackBar();
			se = new TrackBar();
			
			update_timer = new Timer(components);
			
			SuspendLayout();
			
			// Music test.
			music_test.Location = new System.Drawing.Point(100, 50);
			music_test.Name = "music_test";
			music_test.Size = new System.Drawing.Size(80, 20);
			music_test.TabIndex = 0;
			music_test.Text = "Play BGM";
			music_test.UseVisualStyleBackColor = true;
			music_test.Click += MusicTest;
			
			// BGS test.
			bgs_test.Location = new System.Drawing.Point(200, 50);
			bgs_test.Name = "bgs_test";
			bgs_test.Size = new System.Drawing.Size(80, 20);
			bgs_test.TabIndex = 0;
			bgs_test.Text = "Play BGS";
			bgs_test.UseVisualStyleBackColor = true;
			bgs_test.Click += BGSTest;
			
			// Music Effect test.
			test_me.Location = new System.Drawing.Point(100, 100);
			test_me.Name = "test_me";
			test_me.Size = new System.Drawing.Size(80, 20);
			test_me.TabIndex = 1;
			test_me.Text = "Play ME";
			test_me.UseVisualStyleBackColor = true;
			test_me.Click += MusicEffectTest;
			
			// Sound Effect test.
			test_se.Location = new System.Drawing.Point(200, 100);
			test_se.Name = "test_se";
			test_se.Size = new System.Drawing.Size(80, 20);
			test_se.TabIndex = 1;
			test_se.Text = "Play SE";
			test_se.UseVisualStyleBackColor = true;
			test_se.Click += EffectTest;
			
			// Master volume trackbar.
			master.Location = new System.Drawing.Point(500, 100);
			master.Name = "master";
			master.Size = new System.Drawing.Size(400, 45);
			master.TabIndex = 2;
			master.Maximum = 100;
			master.Value = 100;
			master.ValueChanged += ChangeMasterVolume;
			
			// BGM volume trackbar.
			bgm.Location = new System.Drawing.Point(500, 200);
			bgm.Name = "bgm";
			bgm.Size = new System.Drawing.Size(400, 45);
			bgm.TabIndex = 2;
			bgm.Maximum = 100;
			bgm.Value = 100;
			bgm.ValueChanged += ChangeBGMVolume;
			
			// BGS volume trackbar.
			bgs.Location = new System.Drawing.Point(500, 300);
			bgs.Name = "bgs";
			bgs.Size = new System.Drawing.Size(400, 45);
			bgs.TabIndex = 2;
			bgs.Maximum = 100;
			bgs.Value = 100;
			bgs.ValueChanged += ChangeBGSVolume;
			
			// ME volume trackbar.
			me.Location = new System.Drawing.Point(500, 400);
			me.Name = "me";
			me.Size = new System.Drawing.Size(400, 45);
			me.TabIndex = 2;
			me.Maximum = 100;
			me.Value = 100;
			me.ValueChanged += ChangeMEVolume;
			
			// SE volume trackbar.
			se.Location = new System.Drawing.Point(500, 500);
			se.Name = "se";
			se.Size = new System.Drawing.Size(400, 45);
			se.TabIndex = 2;
			se.Maximum = 100;
			se.Value = 100;
			se.ValueChanged += ChangeSEVolume;
			
			// Update timer.
			update_timer.Enabled = true;
			update_timer.Tick += Step;
			
			// Main form.
			ClientSize = new System.Drawing.Size(1280, 720);
			Controls.Add(music_test);
			Controls.Add(bgs_test);
			Controls.Add(test_me);
			Controls.Add(test_se);
			Controls.Add(master);
			Controls.Add(bgm);
			Controls.Add(bgs);
			Controls.Add(me);
			Controls.Add(se);
			Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			MaximizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Rape Engine";
			
			ResumeLayout(false);
		}
	}
}
