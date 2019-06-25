﻿using System;
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
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void Step(object sender, EventArgs args) {
			Audio.Update(0.1);
		}
		
		/// <summary>
		/// Music test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void MusicTest(object sender, EventArgs args) {
			if (!Audio.IsBGMPlaying) {
				Audio.PlayBGM("test");
			} else {
				Audio.StopBGM();
			}
		}
		
		/// <summary>
		/// BGS test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void BGSTest(object sender, EventArgs args) {
			if (!Audio.IsBGSPlaying) {
				Audio.PlayBGS("test");
			} else {
				Audio.StopBGS();
			}
		}
		
		/// <summary>
		/// Music effect test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void MusicEffectTest(object sender, EventArgs args) {
			Audio.PlayME("test");
		}
		
		/// <summary>
		/// Sound Effect test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void EffectTest(object sender, EventArgs args) {
			Audio.PlaySE("test");
		}
		
		void ChangeMasterVolume(object sender, EventArgs args) {
			Audio.VolumeMaster = (double) master.Value / 100;
		}
		
		void ChangeBGMVolume(object sender, EventArgs args) {
			Audio.VolumeBGM = (double) bgm.Value / 100;
		}
		
		void ChangeBGSVolume(object sender, EventArgs args) {
			Audio.VolumeBGS = (double) bgs.Value / 100;
		}
		
		void ChangeMEVolume(object sender, EventArgs args) {
			Audio.VolumeME = (double) me.Value / 100;
		}
		
		void ChangeSEVolume(object sender, EventArgs args) {
			Audio.VolumeSE = (double) se.Value / 100;
		}
	}
}
