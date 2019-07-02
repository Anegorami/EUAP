using System;
using System.Windows.Forms;
using RapeEngine.Components;

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
			AudioManager.Init(Handle);
		}
		
		/// <summary>
		/// Update method. SHOULD be replaced with one-per-frame update event.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void Step(object sender, EventArgs args) {
			AudioManager.Update(update_timer.Interval);
		}
		
		/// <summary>
		/// Music test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void MusicTest(object sender, EventArgs args) {
			if (!AudioManager.IsBGMPlaying) {
				AudioManager.PlayBGM("test");
			} else {
				AudioManager.StopBGM();
			}
		}
		
		/// <summary>
		/// BGS test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void BGSTest(object sender, EventArgs args) {
			if (!AudioManager.IsBGSPlaying) {
				AudioManager.PlayBGS("test");
			} else {
				AudioManager.StopBGS();
			}
		}
		
		/// <summary>
		/// Music effect test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void MusicEffectTest(object sender, EventArgs args) {
			if (!AudioManager.IsMEPlaying) {
				AudioManager.PlayME("test");
			} else {
				AudioManager.StopME();
			}
		}
		
		/// <summary>
		/// Sound Effect test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void EffectTest(object sender, EventArgs args) {
			AudioManager.PlaySE("test");
		}
		
		/// <summary>
		/// Voice test method.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void VoiceTest(object sender, EventArgs args) {
			if (!AudioManager.IsVOPlaying) {
				AudioManager.PlayVO("test");
			} else {
				AudioManager.StopVO();
			}
		}
		
		/// <summary>
		/// A method to relay the master volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeMasterVolume(object sender, EventArgs args) {
			AudioManager.VolumeMaster = (float) master.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the BGM volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeBGMVolume(object sender, EventArgs args) {
			AudioManager.VolumeBGM = (float) bgm.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the BGS volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeBGSVolume(object sender, EventArgs args) {
			AudioManager.VolumeBGS = (float) bgs.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the ME volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeMEVolume(object sender, EventArgs args) {
			AudioManager.VolumeME = (float) me.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the SE volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeSEVolume(object sender, EventArgs args) {
			AudioManager.VolumeSE = (float) se.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the VO volume change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeVOVolume(object sender, EventArgs args) {
			AudioManager.VolumeVO = (float) vo.Value / 100;
		}
		
		/// <summary>
		/// A method to relay the VO modifier change.
		/// </summary>
		/// <param name="sender">Delegate parameter. Required, but not used.</param>
		/// <param name="args">Delegate parameter. Required, but not used.</param>
		void ChangeVOModifier(object sender, EventArgs args) {
			AudioManager.VOModifier = (float) vom.Value / 100;
		}
	}
}
