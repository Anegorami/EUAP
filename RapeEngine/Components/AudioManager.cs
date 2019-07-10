using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using Un4seen.Bass;

using RapeEngine.Resources;

namespace RapeEngine.Components {
	/// <summary>
	/// Static class for the audio-related operations.
	/// </summary>
	public static class AudioManager {
		/// <summary>
		/// Enumerator for channels.
		/// </summary>
		enum TYPE {BGM, BGS, ME, VO, SE}
		
		/// <summary>
		/// Error message when trying to play a non-existent sample.
		/// </summary>
		const string ERROR_SAMPLE_NOT_FOUND_MESSAGE = "{0} with filename \"{1}\" wasn't found!\n" +
			"Make sure that the file is present in the \"{2}\" folder!";
		
		/// <summary>
		/// Dictionary for all of the Audio Samples.
		/// </summary>
		static readonly Dictionary<TYPE, Dictionary<string, AudioSample>> audio =
			new Dictionary<TYPE, Dictionary<string, AudioSample>>();
		
		/// <summary>
		/// Dictionary for the active Audio Samples.
		/// </summary>
		static readonly Dictionary<TYPE, AudioSample> active = new Dictionary<TYPE, AudioSample>();
		
		/// <summary>
		/// List with the active SEs.
		/// </summary>
		static readonly List<AudioSample> active_se = new List<AudioSample>();
		
		/// <summary>
		/// Dictionary with the channel volumes.
		/// </summary>
		static readonly Dictionary<TYPE, float> volume = new Dictionary<TYPE, float>();
		
		/// <summary>
		/// List for the Audio Effects.
		/// </summary>
		static readonly List<AudioEffect> effects = new List<AudioEffect>();
		
		/// <summary>
		/// Dictionary with directories to load the samples from.
		/// </summary>
		static readonly Dictionary<TYPE, string> dir = new Dictionary<TYPE, string>();
		
		/// <summary>
		/// A field to check if the BGM is playing.
		/// </summary>
		public static bool IsBGMPlaying {
			get {
				return IsPlaying(TYPE.BGM);
			}
		}
		
		/// <summary>
		/// A field to check if the BGS is playing.
		/// </summary>
		public static bool IsBGSPlaying {
			get {
				return IsPlaying(TYPE.BGS);
			}
		}
		
		/// <summary>
		/// A field to check if the ME is playing.
		/// </summary>
		public static bool IsMEPlaying {
			get {
				return IsPlaying(TYPE.ME);
			}
		}
		
		/// <summary>
		/// A field to check if the VO is playing.
		/// </summary>
		public static bool IsVOPlaying {
			get {
				return IsPlaying(TYPE.VO);
			}
		}
		
		/// <summary>
		/// A field to check if at least one SE is playing.
		/// </summary>
		public static bool IsSEPlaying {
			get {
				return active_se.Count > 0;
			}
		}
		
		/// <summary>
		/// Master Volume field.
		/// </summary>
		public static float VolumeMaster {get; set;}
		
		/// <summary>
		/// BGM volume field.
		/// </summary>
		public static float VolumeBGM {
			get {
				return volume[TYPE.BGM];
			}
			set {
				volume[TYPE.BGM] = value;
			}
		}
		
		/// <summary>
		/// BGS volume field.
		/// </summary>
		public static float VolumeBGS {
			get {
				return volume[TYPE.BGS];
			}
			set {
				volume[TYPE.BGS] = value;
			}
		}
		
		/// <summary>
		/// ME volume field.
		/// </summary>
		public static float VolumeME {
			get {
				return volume[TYPE.ME];
			}
			set {
				volume[TYPE.ME] = value;
			}
		}
		
		/// <summary>
		/// VO volume field.
		/// </summary>
		public static float VolumeVO {
			get {
				return volume[TYPE.VO];
			}
			set {
				volume[TYPE.VO] = value;
			}
		}
		
		/// <summary>
		/// SE volume field.
		/// </summary>
		public static float VolumeSE {
			get {
				return volume[TYPE.SE];
			}
			set {
				volume[TYPE.SE] = value;
			}
		}
		
		/// <summary>
		/// VO BGM/BGS volume modifier field.
		/// </summary>
		public static float VOModifier = 0.2f;
		
		/// <summary>
		/// Method to check if the channel have a playing sample.
		/// </summary>
		/// <param name="type">Channel type.</param>
		/// <returns>True if the channel have an active sample, false otherwise.</returns>
		static bool IsPlaying(TYPE type) {
			return active.ContainsKey(type) && active[type].IsPlaying;
		}
		
		/// <summary>
		/// Error message when trying to play a non-existent sample.
		/// </summary>
		/// <param name="type">Channel type.</param>
		/// <param name="key">Sample name.</param>
		static void ErrorSampleNotFound(TYPE type, string key) {
			// Naming items.
			string item = "";
			switch (type) {
				case (TYPE.BGM):
					item = "BGM";
					break;
				case (TYPE.BGS):
					item = "BGS";
					break;
				case (TYPE.ME):
					item = "ME";
					break;
				case (TYPE.VO):
					item = "VO";
					break;
				case (TYPE.SE):
					item = "SE";
					break;
			}
			
			key += ".ogg";
			
			string message = String.Format(ERROR_SAMPLE_NOT_FOUND_MESSAGE, item, key, dir[type]);
			
			MessageBox.Show(message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		
		/// <summary>
		/// A method to play a sample.
		/// </summary>
		/// <param name="type">Channel type.</param>
		/// <param name="key">Dictionary key.</param>
		static void Play(TYPE type, string key) {
			try {
				audio[type][key].Play();
			} catch (KeyNotFoundException) {
				ErrorSampleNotFound(type, key);
				return;
			}
			active[type] = audio[type][key];
		}
		
		/// <summary>
		/// A method to stop an active sample.
		/// </summary>
		/// <param name="type">Channel type.</param>
		static void Stop(TYPE type) {
			if (active.ContainsKey(type)) {
				active[type].Stop();
			}
		}
		
		/// <summary>
		/// Predicate for the removal of the stopped SEs.
		/// </summary>
		/// <param name="sample">Sample to test.</param>
		/// <returns>True if the sample is playing, false otherwise.</returns>
		static bool IsAudioSamplePlaying(AudioSample sample) {
			return sample.IsPlaying;
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <param name="handle">Main window handle. Required for the BASS library initialization.</param>
		public static void Init(IntPtr handle) {
			// BASS library initialization.
			// -1 is the default audio device.
			// 48000 is the sample rate.
			// The "handle" is the main window's handle to bind the audio device to (for Windows Audio Mixer).
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, handle);
			
			// Directory setup.
			dir[TYPE.BGM] = "bgm";
			dir[TYPE.BGS] = "bgs";
			dir[TYPE.ME] = "me";
			dir[TYPE.VO] = "vo";
			dir[TYPE.SE] = "se";
			
			foreach (TYPE type in Enum.GetValues(typeof(TYPE))) {
				try {
					foreach (string filename in Directory.GetFiles(dir[type])) {
						// Enable looping for BGMs and BGSs.
						bool looping = ((type == TYPE.BGM) || (type == TYPE.BGS));
						
						// Give several channels to SEs.
						int channels = (type == TYPE.SE)? 10: 1;
						
						if (!audio.ContainsKey(type)) {
							audio[type] = new Dictionary<string, AudioSample>();
						}
						audio[type][Negolib.MakeKey(filename)] = new AudioSample(filename, looping, channels);
					}
				} catch (DirectoryNotFoundException) {
					Directory.CreateDirectory(dir[type]);
				}
				
				volume[type] = 1;
			}
			
			VolumeMaster = 1;
		}
		
		/// <summary>
		/// An update method. Should be bound to Graphic.OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame (in milliseconds).</param>
		public static void Update(int dt) {
			// Volume manipulation.
			foreach (TYPE type in Enum.GetValues(typeof(TYPE))) {
				if (active.ContainsKey(type)) {
					active[type].Update();
					active[type].Volume = volume[type] * VolumeMaster;
					
					// Special case - reduce BGM and BGS volume if a VO is playing.
					if (((type == TYPE.BGM) || (type == TYPE.BGS)) && (IsVOPlaying)) {
						active[type].Volume *= VOModifier;
					}
				}
			}
			
			// Active SE update and cleaning.
			foreach (AudioSample sample in active_se) {
				sample.Update();
			}
			active_se.RemoveAll(IsAudioSamplePlaying);
			
			// Effect update and cleaning.
			foreach (AudioEffect effect in effects) {
				effect.Update(dt);
			}
			effects.RemoveAll(AudioEffect.RemovalCondition);
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="key">Filename without the extension.</param>
		public static void PlayBGM(string key) {
			Stop(TYPE.BGM);
			Stop(TYPE.ME);
			
			Play(TYPE.BGM, key);
		}
		
		/// <summary>
		/// A method to play the background sounds.
		/// </summary>
		/// <param name="key">Filename without the extension.</param>
		public static void PlayBGS(string key) {
			Stop(TYPE.BGS);
			Stop(TYPE.ME);
			
			Play(TYPE.BGS, key);
		}
		
		/// <summary>
		/// A method to play a music effect.
		/// </summary>
		/// <param name="key">Filename without the extension.</param>
		/// <param name="time">Time of the BGM and BGS fadein (in milliseconds).</param>
		public static void PlayME(string key, int time = 3000) {
			Stop(TYPE.ME);
			Stop(TYPE.VO);
			
			if (active.ContainsKey(TYPE.BGM)) {
				effects.Add(new AudioFadeIn(active[TYPE.BGM], time));
			}
			
			if (active.ContainsKey(TYPE.BGS)) {
				effects.Add(new AudioFadeIn(active[TYPE.BGS], time));
			}
			
			Play(TYPE.ME, key);
		}
		
		/// <summary>
		/// A method to play a voice file.
		/// </summary>
		/// <param name="key">Filename without the extension.</param>
		public static void PlayVO(string key) {
			Stop(TYPE.VO);
			Play(TYPE.VO, key);
		}
		
		/// <summary>
		/// A method to play the sound effect.
		/// </summary>
		/// <param name="key">Filename without the extension.</param>
		public static void PlaySE(string key) {
			Play(TYPE.SE, key);
			if (active.ContainsKey(TYPE.SE)) {
				active_se.Add(active[TYPE.SE]);
			}
		}
		
		/// <summary>
		/// A method to stop the currently playing BGM.
		/// </summary>
		/// <param name="time">Fade time (in milliseconds).</param>
		public static void StopBGM(int time = 3000) {
			effects.Add(new AudioFadeOut(active[TYPE.BGM], time));
		}
		
		/// <summary>
		/// A method to stop the currently playing BGS.
		/// </summary>
		/// <param name="time">Fade time (in milliseconds).</param>
		public static void StopBGS(int time = 3000) {
			effects.Add(new AudioFadeOut(active[TYPE.BGS], time));
		}
		
		/// <summary>
		/// A method to stop the currently playing ME.
		/// </summary>
		public static void StopME() {
			Stop(TYPE.ME);
		}
		
		/// <summary>
		/// A method to stop the voice file.
		/// </summary>
		public static void StopVO() {
			Stop(TYPE.VO);
		}
		
		/// <summary>
		/// A method to stop all active SEs.
		/// </summary>
		public static void StopSE() {
			foreach (AudioSample sample in active_se) {
				sample.Stop();
			}
		}
	}
}
