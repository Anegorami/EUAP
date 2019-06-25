using System;
using System.IO;
using System.Collections.Generic;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace RapeEngine {
	/// <summary>
	/// Static class for audio-related operations.
	/// </summary>
	public static class Audio {
		/// <summary>
		/// Small structure to keep all of the sample-related data in one place.
		/// </summary>
		struct LoopedSample {
			/// <summary>
			/// Sample handle.
			/// </summary>
			public readonly int Sample;
			
			/// <summary>
			/// Position of the beginning of the loop (in seconds).
			/// </summary>
			public readonly double LoopStart;
			
			/// <summary>
			/// Position of the end of the loop (in seconds).
			/// </summary>
			public readonly double LoopEnd;
			
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="filename">File name.</param>
			public LoopedSample(string filename) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// The 1 is the maximum channels playing the same sample simulteniously.
				// For looped samples, 1 is a good choice.
				Sample = Bass.BASS_SampleLoad(filename, 0, 0, 1, BASSFlag.BASS_DEFAULT);
				
				// Extracting loop timepoints from the tags.
				TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(filename);
				string test = tag.NativeTag("loop_start");
				if (test != null) {
					LoopStart = Negolib.StringToDouble(test);
					LoopEnd = Negolib.StringToDouble(tag.NativeTag("loop_end"));
				} else {
					LoopStart = -1;
					LoopEnd = Double.PositiveInfinity;
				}
			}
		}
		
		/// <summary>
		/// Dictionary for storing the loaded BGM samples.
		/// </summary>
		static readonly Dictionary<string, LoopedSample> bgm = new Dictionary<string, LoopedSample>();
		
		/// <summary>
		/// Active BGM.
		/// </summary>
		static LoopedSample bgm_current;
		
		/// <summary>
		/// Active BGM channel handle.
		/// </summary>
		static int bgm_channel;
		
		/// <summary>
		/// Fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgm_fade_timer;
		
		/// <summary>
		/// Initial value of the fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgm_fade_timer_max;
		
		/// <summary>
		/// Dictionary for storing the loaded BGS samples.
		/// </summary>
		static readonly Dictionary<string, LoopedSample> bgs = new Dictionary<string, LoopedSample>();
		
		/// <summary>
		/// Active BGS.
		/// </summary>
		static LoopedSample bgs_current;
		
		/// <summary>
		/// Active BGS channel handle.
		/// </summary>
		static int bgs_channel;
		
		/// <summary>
		/// Fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgs_fade_timer;
		
		/// <summary>
		/// Initial value of the fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgs_fade_timer_max;
		
		/// <summary>
		/// Dictionary to store the loaded ME samples' handles.
		/// </summary>
		static readonly Dictionary<string, int> me = new Dictionary<string, int>();
		
		/// <summary>
		/// Active ME channel handle.
		/// </summary>
		static int me_channel;
		
		/// <summary>
		/// Dictionary to store the loaded SE samples' handles.
		/// </summary>
		static readonly Dictionary<string, int> se = new Dictionary<string, int>();
		
		/// <summary>
		/// BGM volume.
		/// </summary>
		static double volume_bgm = 1;
		
		/// <summary>
		/// BGS volume.
		/// </summary>
		static double volume_bgs = 1;
		
		/// <summary>
		/// ME volume.
		/// </summary>
		static double volume_me = 1;
		
		/// <summary>
		/// SE volume.
		/// </summary>
		static double volume_se = 1;
		
		/// <summary>
		/// Delegate for an update action.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		delegate void UpdateAction(double dt);
		
		/// <summary>
		/// Update event. Called in the Update method.
		/// </summary>
		static event UpdateAction OnUpdate;
		
		/// <summary>
		/// A field to check if the BGM is playing.
		/// </summary>
		public static bool IsBGMPlaying {
			get {
				return IsPlaying(bgm_channel);
			}
		}
		
		/// <summary>
		/// A field to check if the BGS is playing.
		/// </summary>
		public static bool IsBGSPlaying {
			get {
				return IsPlaying(bgs_channel);
			}
		}
		
		/// <summary>
		/// A field to check if the ME is playing.
		/// </summary>
		public static bool IsMEPlaying {
			get {
				return IsPlaying(me_channel);
			}
		}
		
		/// <summary>
		/// Master volume field.
		/// </summary>
		public static double VolumeMaster {
			get {
				return (double) Bass.BASS_GetVolume();
			}
			set {
				Bass.BASS_SetVolume((float) value);
			}
		}
		
		/// <summary>
		/// BGM volume field.
		/// </summary>
		public static double VolumeBGM {
			get {
				return volume_bgm;
			}
			set {
				volume_bgm = value;
				SetVolume(bgm_channel, value);
			}
		}
		
		/// <summary>
		/// BGS volume field.
		/// </summary>
		public static double VolumeBGS {
			get {
				return volume_bgs;
			}
			set {
				volume_bgs = value;
				SetVolume(bgs_channel, value);
			}
		}
		
		/// <summary>
		/// ME volume field.
		/// </summary>
		public static double VolumeME {
			get {
				return volume_me;
			}
			set {
				volume_me = value;
				SetVolume(me_channel, value);
			}
		}
		
		/// <summary>
		/// SE volume field.
		/// </summary>
		public static double VolumeSE {
			get {
				return volume_se;
			}
			set {
				volume_se = value;
			}
		}
		
		/// <summary>
		/// A method to check if the channel is playing.
		/// </summary>
		/// <param name="channel">Channel to check.</param>
		/// <returns>True, if the channel is playing, false otherwise.</returns>
		static bool IsPlaying(int channel) {
			return Bass.BASS_ChannelIsActive(channel) == BASSActive.BASS_ACTIVE_PLAYING;
		}
		
		/// <summary>
		/// A method to loop a LoopedSample.
		/// </summary>
		/// <param name="current">LoopedSample to loop.</param>
		/// <param name="channel">Channel to modify.</param>
		static void Looping(LoopedSample current, int channel) {
			long current_position_bytes = Bass.BASS_ChannelGetPosition(channel);
			double current_position = Bass.BASS_ChannelBytes2Seconds(channel, current_position_bytes);
			if (current_position > current.LoopEnd) {
				double new_position = current.LoopStart + (current_position - current.LoopEnd);
				Bass.BASS_ChannelSetPosition(channel, new_position);
			}
		}
		
		/// <summary>
		/// A method to change channel's volume.
		/// </summary>
		/// <param name="channel">Channel to change volume on.</param>
		/// <param name="volume">Volume level. Must be in between 0 (mute) and 1 (100% volume).</param>
		static void SetVolume(int channel, double volume) {
			Bass.BASS_ChannelSetAttribute(channel, BASSAttribute.BASS_ATTRIB_VOL, (float) volume);
		}
		
		/// <summary>
		/// A method to fadeout the BGM.
		/// Should be binded to the OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGMFadeOut(double dt) {
			bgm_fade_timer = Math.Max(bgm_fade_timer - dt, 0);
			double volume = (bgm_fade_timer / bgm_fade_timer_max) * volume_bgm;
			SetVolume(bgm_channel, volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				Bass.BASS_ChannelStop(bgm_channel);
				OnUpdate -= BGMFadeOut;
			}
		}
		
		/// <summary>
		/// A method to fadein the BGM.
		/// Should be binded to the OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGMFadeIn(double dt) {
			if (IsMEPlaying) {
				return;
			}
			
			if (!IsBGMPlaying) {
				Bass.BASS_ChannelPlay(bgm_channel, false);
			}
			
			bgm_fade_timer = Math.Max(bgm_fade_timer - dt, 0);
			double volume = (bgm_fade_timer / bgm_fade_timer_max) / volume_bgm;
			SetVolume(bgm_channel, volume_bgm - volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				OnUpdate -= BGMFadeIn;
			}
		}
		
		/// <summary>
		/// A method to fadeout the BGS.
		/// Should be binded to the OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGSFadeOut(double dt) {
			bgs_fade_timer = Math.Max(bgs_fade_timer - dt, 0);
			double volume = (bgs_fade_timer / bgs_fade_timer_max) * volume_bgs;
			SetVolume(bgs_channel, volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				Bass.BASS_ChannelStop(bgs_channel);
				OnUpdate -= BGSFadeOut;
			}
		}
		
		/// <summary>
		/// A method to fadein the BGS.
		/// Should be binded to the OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGSFadeIn(double dt) {
			if (IsMEPlaying) {
				return;
			}
			
			if (!IsBGSPlaying) {
				Bass.BASS_ChannelPlay(bgs_channel, false);
			}
			
			bgs_fade_timer = Math.Max(bgs_fade_timer - dt, 0);
			double volume = (bgs_fade_timer / bgs_fade_timer_max) / volume_bgs;
			SetVolume(bgs_channel, volume_bgs - volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				OnUpdate -= BGSFadeIn;
			}
		}
		
		/// <summary>
		/// A method to enable auto looping (i.e. loop the whole sample).
		/// </summary>
		/// <param name="channel">Channel to enable the looping on.</param>
		static void ActivateAutoLooping(int channel) {
			Bass.BASS_ChannelFlags(channel, BASSFlag.BASS_SAMPLE_LOOP, BASSFlag.BASS_SAMPLE_LOOP);
		}
		
		/// <summary>
		/// Initialization function.
		/// </summary>
		/// <param name="handle">Main window handle. Required for the BASS library initialization.</param>
		public static void Init(IntPtr handle) {
			// BASS library initialization.
			// -1 is the default audio device.
			// 48000 is the sample rate.
			// The "handle" is the main window's handle to bind the audio device to (for Windows Audio Mixer).
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, handle);
			
			// BGM folder scanning.
			foreach (string filename in Directory.GetFiles("bgm")) {
				bgm[Negolib.MakeKey(filename)] = new LoopedSample(filename);
			}
			
			// BGS folder scanning.
			foreach (string filename in Directory.GetFiles("bgs")) {
				bgs[Negolib.MakeKey(filename)] = new LoopedSample(filename);
			}
			
			// ME folder scanning.
			foreach (string filename in Directory.GetFiles("me")) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// 1 is an amount af channels that can play the same sample simulteniously.
				me[Negolib.MakeKey(filename)] = Bass.BASS_SampleLoad(filename, 0, 0, 1, BASSFlag.BASS_DEFAULT);
			}
			
			// SE folder scanning.
			foreach (string filename in Directory.GetFiles("se")) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// 10 is an amount af channels that can play the same sample simulteniously.
				se[Negolib.MakeKey(filename)] = Bass.BASS_SampleLoad(filename, 0, 0, 10, BASSFlag.BASS_DEFAULT);
			}
		}
		
		/// <summary>
		/// An update method. Should be bound to Graphic.OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		public static void Update(double dt) {
			Looping(bgm_current, bgm_channel);
			Looping(bgs_current, bgs_channel);
			if (OnUpdate != null) {
				OnUpdate(dt);
			}
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="bgmname">Filename without the extension.</param>
		public static void PlayBGM(string bgmname) {
			// Stops any current BGM and frees the channel.
			Bass.BASS_ChannelStop(bgm_channel);
			
			bgm_current = bgm[bgmname];
			bgm_channel = Bass.BASS_SampleGetChannel(bgm_current.Sample, false);
			
			Bass.BASS_ChannelPlay(bgm_channel, false);
			SetVolume(bgm_channel, volume_bgm);
			
			if (Math.Abs(bgm_current.LoopStart + 1) < Double.Epsilon) {
				ActivateAutoLooping(bgm_channel);
			}
		}
		
		/// <summary>
		/// A method to stop the currently playing BGM.
		/// </summary>
		/// <param name="time">Fade time (in seconds).</param>
		public static void StopBGM (double time = 3) {
			bgm_fade_timer = time;
			bgm_fade_timer_max = time;
			OnUpdate += BGMFadeOut;
		}
		
		/// <summary>
		/// A method to play the background sounds.
		/// </summary>
		/// <param name="bgsname">Filename without the extension.</param>
		public static void PlayBGS(string bgsname) {
			// Stops any current BGS and frees the channel.
			Bass.BASS_ChannelStop(bgs_channel);
			
			bgs_current = bgs[bgsname];
			bgs_channel = Bass.BASS_SampleGetChannel(bgs_current.Sample, false);
			
			Bass.BASS_ChannelPlay(bgs_channel, false);
			SetVolume(bgm_channel, volume_bgs);
			
			if (Math.Abs(bgs_current.LoopStart + 1) < Double.Epsilon) {
				ActivateAutoLooping(bgs_channel);
			}
		}
		
		/// <summary>
		/// A method to stop the currently playing BGS.
		/// </summary>
		/// <param name="time">Fade time (in seconds).</param>
		public static void StopBGS (double time = 3) {
			bgs_fade_timer = time;
			bgs_fade_timer_max = time;
			OnUpdate += BGSFadeOut;
		}
		
		/// <summary>
		/// A method to play a music effect.
		/// </summary>
		/// <param name="mename">Filename without the extension.</param>
		/// <param name="time">Time of the BGM and BGS fadein (in seconds).</param>
		public static void PlayME(string mename, double time = 3) {
			if (IsBGMPlaying) {
				Bass.BASS_ChannelPause(bgm_channel);
				SetVolume(bgm_channel, 0);
				bgm_fade_timer = time;
				bgm_fade_timer_max = time;
				OnUpdate += BGMFadeIn;
			}
			
			if (IsBGSPlaying) {
				Bass.BASS_ChannelPause(bgs_channel);
				SetVolume(bgs_channel, 0);
				bgs_fade_timer = time;
				bgs_fade_timer_max = time;
				OnUpdate += BGSFadeIn;
			}
			
			me_channel = Bass.BASS_SampleGetChannel(me[mename], false);
			Bass.BASS_ChannelPlay(me_channel, false);
			SetVolume(me_channel, volume_me);
		}
		
		/// <summary>
		/// A method to play the sound effect.
		/// </summary>
		/// <param name="sename">Filename without the extension.</param>
		public static void PlaySE(string sename) {
			int channel = Bass.BASS_SampleGetChannel(se[sename], false);
			Bass.BASS_ChannelPlay(channel, false);
			SetVolume(channel, volume_se);
		}
	}
}
