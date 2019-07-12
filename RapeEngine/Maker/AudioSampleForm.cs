using System;
using System.Collections.Generic;
using System.Windows.Forms;

using RapeEngine.Components;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form for selecting an audio sample.
	/// </summary>
	public partial class AudioSampleForm: Form {
		/// <summary>
		/// Form mode enumerator.
		/// </summary>
		enum MODE {ALL, BGM, BGS, ME, VO, SE}
		
		/// <summary>
		/// Current form mode.
		/// </summary>
		readonly MODE mode;
		
		/// <summary>
		/// Form value.
		/// </summary>
		public string Value {
			get {
				switch (mode) {
					case (MODE.BGM):
						return (string)list_bgm.SelectedItem;
					case (MODE.BGS):
						return (string)list_bgs.SelectedItem;
					case (MODE.ME):
						return (string)list_me.SelectedItem;
					case (MODE.VO):
						return (string)list_vo.SelectedItem;
					case (MODE.SE):
						return (string)list_se.SelectedItem;
					default:
						return "";
				}
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_mode">Mode that the form should work in.</param>
		/// <param name="key">Initialy selected key.</param>
		AudioSampleForm(MODE _mode = MODE.ALL, string key = "") {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			mode = _mode;
			
			FillSampleList(list_bgm, AudioManager.GetBGMKeys(), tab_bgm, key);
			FillSampleList(list_bgs, AudioManager.GetBGSKeys(), tab_bgs, key);
			FillSampleList(list_me, AudioManager.GetMEKeys(), tab_me, key);
			FillSampleList(list_vo, AudioManager.GetVOKeys(), tab_vo, key);
			FillSampleList(list_se, AudioManager.GetSEKeys(), tab_se, key);
			
			switch (mode) {
				case (MODE.BGM):
					tabs.TabPages.Clear();
					tabs.TabPages.Add(tab_bgm);
					break;
				case (MODE.BGS):
					tabs.TabPages.Clear();
					tabs.TabPages.Add(tab_bgs);
					break;
				case (MODE.ME):
					tabs.TabPages.Clear();
					tabs.TabPages.Add(tab_me);
					break;
				case (MODE.VO):
					tabs.TabPages.Clear();
					tabs.TabPages.Add(tab_vo);
					break;
				case (MODE.SE):
					tabs.TabPages.Clear();
					tabs.TabPages.Add(tab_se);
					break;
			}
			
			// If the form was used as browser or no samples found - block OK button.
			if ((mode == MODE.ALL) || (!tabs.TabPages[0].Controls[0].Enabled)) {
				button_ok.Enabled = false;
				button_ok.Text = "";
				button_cancel.Text = "Close";
			}
		}
		
		/// <summary>
		/// Method for retrieval a form instance in browser mode.
		/// </summary>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetInstance() {
			return new AudioSampleForm();
		}
		
		/// <summary>
		/// Method for retrieval a form instance in BGM selection mode.
		/// </summary>
		/// <param name="key">Initialy selected key.</param>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetBGMInstance(string key) {
			return new AudioSampleForm(MODE.BGM, key);
		}
		
		/// <summary>
		/// Method for retrieval a form instance in BGS selection mode.
		/// </summary>
		/// <param name="key">Initialy selected key.</param>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetBGSInstance(string key) {
			return new AudioSampleForm(MODE.BGS, key);
		}
		
		/// <summary>
		/// Method for retrieval a form instance in ME selection mode.
		/// </summary>
		/// <param name="key">Initialy selected key.</param>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetMEInstance(string key) {
			return new AudioSampleForm(MODE.ME, key);
		}
		
		/// <summary>
		/// Method for retrieval a form instance in VO selection mode.
		/// </summary>
		/// <param name="key">Initialy selected key.</param>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetVOInstance(string key) {
			return new AudioSampleForm(MODE.VO, key);
		}
		
		/// <summary>
		/// Method for retrieval a form instance in SE selection mode.
		/// </summary>
		/// <param name="key">Initialy selected key.</param>
		/// <returns>Form instance.</returns>
		public static AudioSampleForm GetSEInstance(string key) {
			return new AudioSampleForm(MODE.SE, key);
		}
		
		/// <summary>
		/// Method for sample lists filling.
		/// </summary>
		/// <param name="target">List to fill.</param>
		/// <param name="keys">Keys to fill the list with.</param>
		/// <param name="page">Page to block in case of no samples being present.</param>
		/// <param name="selected_key">Initialy selected key.</param>
		void FillSampleList(ListBox target, List<string> keys, Control page, string selected_key) {
			foreach (string key in keys) {
				target.Items.Add(key);
				if (key == selected_key) {
					target.SelectedIndex = target.Items.Count - 1;
				}
			}
			
			if (target.Items.Count == 0) {
				foreach (Control control in page.Controls) {
					control.Enabled = false;
				}
			}
		}
		
		/// <summary>
		/// Response for clicking play BGM button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_play_bgmClick(object sender, EventArgs e) {
			AudioManager.PlayBGM((string)list_bgm.SelectedItem);
		}
		
		/// <summary>
		/// Response for clicking stop BGM button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_stop_bgmClick(object sender, EventArgs e) {
			AudioManager.StopBGM();
		}
		
		/// <summary>
		/// Response for clicking play BGS button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_play_bgsClick(object sender, EventArgs e) {
			AudioManager.PlayBGS((string)list_bgs.SelectedItem);
		}
		
		/// <summary>
		/// Response for clicking stop BGS button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_stop_bgsClick(object sender, EventArgs e) {
			AudioManager.StopBGS();
		}
		
		/// <summary>
		/// Response for clicking play ME button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_play_meClick(object sender, EventArgs e) {
			AudioManager.PlayME((string)list_me.SelectedItem);
		}
		
		/// <summary>
		/// Response for clicking stop ME button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_stop_meClick(object sender, EventArgs e) {
			AudioManager.StopME();
		}
		
		/// <summary>
		/// Response for clicking play VO button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_play_voClick(object sender, EventArgs e) {
			AudioManager.PlayVO((string)list_vo.SelectedItem);
		}
		
		/// <summary>
		/// Response for clicking stop VO button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_stop_voClick(object sender, EventArgs e) {
			AudioManager.StopVO();
		}
		
		/// <summary>
		/// Response for clicking play SE button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_play_seClick(object sender, EventArgs e) {
			AudioManager.PlaySE((string)list_se.SelectedItem);
		}
		
		/// <summary>
		/// Response for clicking stop SE button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_stop_seClick(object sender, EventArgs e) {
			AudioManager.StopSE();
		}
	}
}
